/*
	Real time spellchecker
	Interface to work with the Firefox 2 Spellchecker for textareas
  
  File Author:
  		Alfonso Martínez de Lizarrondo amla70 at gmail dot com

	version 0.7 - 18/05/2008
		Public version


Usage:
1. 
	Extract the plugin file under the plugins directory so it ends up as editor/plugins/geckospellchecker/fckplugin.js

2.
	Include the plugin in your config file:
	FCKConfig.Plugins.Add( 'geckospellchecker' ) ;


*/

var GeckoSpellchecker = {

	// This function tries to call the internal Firefox spellchecker in order to get suggestions for the current click
	// it needs that the Write Area extension is already installed because by default there's no such call system in place.
	fillSuggestions : function( ev )
	{
		// Init to no errors or no spellchecker available.
		this.mSpellSuggestions = null ;

		if (document.location.protocol == 'chrome:')
			this._callInternalSpellcheker( ev ) ;
		else
			this._callExtensionSpellcheker( ev ) ;

	},

	// Called from web page, create a fake event to get the suggestions with our extension
	_callExtensionSpellcheker : function(ev) 
	{
		// Create our custom event 'nsDOMQuerySpellchecker'
		var node = ev.target ;
		var doc = node.ownerDocument ;
		var newEv = doc.createEvent( 'MouseEvent' ) ;
		newEv.initMouseEvent( 'nsDOMQuerySpellchecker', true, true, doc.defaultView, ev.detail,
						ev.screenX, ev.screenY, ev.clientX, ev.clientY, ev.ctrlKey, ev.altKey,
						ev.shiftKey, ev.metaKey, ev.button, ev.relatedTarget ) ;

		// The rangeOffset doesn't get cloned, so we need to copy it in a custom attribute.
		node.setAttribute( 'myRangeOffset', ev.rangeOffset ) ;

		// Store the data to replace the misspelled word
		this.mWordNode = ev.rangeParent ;
		this.mWordOffset = ev.rangeOffset ; 

		// Now this is where the magic happens. The event goes up and when it's been processed we will have the answer
		node.dispatchEvent( newEv ) ;

		// First remove the previous attribute:
		node.removeAttribute( 'myRangeOffset' ) ;

		// Check if there was a misspelling
		this.misspelling = node.getAttribute( 'spellcheck_misspelling')
		// The call has failed, the extension isn't installed
		if ( this.misspelling === null )
			return ;
		node.removeAttribute( 'spellcheck_misspelling' ) ;
		// Ok, the word is right.
		if (this.misspelling === '')
			return ;

		// Get the suggestions and store them in an array.
		this.mSpellSuggestions = [] ;

		var nSuggestions = parseInt( node.getAttribute( 'spellcheck_suggestions' ), 10) ;
		node.removeAttribute( 'spellcheck_suggestions' ) ;

		for( var i=0; i<nSuggestions; i++ )
		{
			var suggestion = node.getAttribute( 'spellcheck_suggestion' + i) ;
			if (suggestion)
			{
				node.removeAttribute( 'spellcheck_suggestion' + i) ;
				this.mSpellSuggestions.push( suggestion ) ;
			}
		}
	},

	// Call inside chrome, the event doesn't arrive to the listener, so let's do it directly.
  _callInternalSpellcheker: function(evt) 
	{
		// Store the data to replace the misspelled word
		this.mWordNode = evt.rangeParent ;
		this.mWordOffset = evt.rangeOffset ; 

		// if the document is editable do our task
		var win = evt.target.ownerDocument.defaultView;
		if (win) {
			var editingSession = win.QueryInterface(Components.interfaces.nsIInterfaceRequestor)
															.getInterface(Components.interfaces.nsIWebNavigation)
															.QueryInterface(Components.interfaces.nsIInterfaceRequestor)
															.getInterface(Components.interfaces.nsIEditingSession);
			if (editingSession.windowIsEditable(win)) {

				InlineSpellCheckerUI.init(editingSession.getEditorForWindow(win));
				InlineSpellCheckerUI.initFromEvent(this.mWordNode, this.mWordOffset);

				this.misspelling = InlineSpellCheckerUI.mMisspelling ;
				if ( !InlineSpellCheckerUI.overMisspelling )
					return ;

				var nSuggestions = 10 ;

				// Get the suggestions and store them in an array.
				this.mSpellSuggestions = this.getSpellcheckSuggestions( nSuggestions ) ;
			}
		}

  }, 

  // Returns an array of up to maxNumber suggestions for the currently misspelled word
	// it can be called only inside Chrome
  getSpellcheckSuggestions: function( maxNumber )
  {
    if (! InlineSpellCheckerUI.mInlineSpellChecker || ! InlineSpellCheckerUI.mOverMisspelling)
      return 0; // nothing to do

    var spellchecker = InlineSpellCheckerUI.mInlineSpellChecker.spellChecker;
    if (! spellchecker.CheckCurrentWord(InlineSpellCheckerUI.mMisspelling))
      return 0;  // word seems not misspelled after all (?)

    var mSpellSuggestions = [];

    for (var i = 0; i < maxNumber; i ++) {
      var suggestion = spellchecker.GetSuggestedWord();
      if (! suggestion.length)
        break;
       mSpellSuggestions.push(suggestion);

    }
    return mSpellSuggestions;
  },


	// Almost like the internal function
  replaceMisspelling: function(index)
  {
    if (! this.mSpellSuggestions)
      return;
    if (index < 0 || index >= this.mSpellSuggestions.length)
      return;
    this.replaceWord(this.mWordNode, this.mWordOffset,
                                         this.mSpellSuggestions[index]);
  },

	// a first approach for this function.
  replaceWord: function(node, offset, replacement)
  {
		var text = node.nodeValue ;

		// Find the word boundaries. Is there a better way?
		// we could use directly text.replace with the misspelled word, but it will fail if there are several instances.
		var regSeparator = /\b/ ;
		var start = offset;
		while (start>=0 && regSeparator.test(text.charAt(start) ) )
			start-- ;
		
		start++;

		var end = offset;
		while (end<text.length && regSeparator.test(text.charAt(end) ) )
			end++ ;

		// Do the replacement
		node.nodeValue = text.substr(0, start) + replacement + text.substr(end);

		// Now set the new word as selected
		var selection =	FCK.EditorWindow.getSelection() ;
		selection.removeAllRanges() ;

		var range = FCK.EditorDocument.createRange() ;
		range.setStart(node, start);
		range.setEnd(node, start + replacement.length) ;
		selection.addRange(range);
	}


}

// Spellsuggestion command
var SpellSuggestion = function() {};

// The button has been pressed, do our work
// we get back the index
SpellSuggestion.prototype.Execute = function( i ) 
{
	FCKUndo.SaveUndoStep() ;
	GeckoSpellchecker.replaceMisspelling( i ) ;

};


// If we are using Firefox then register now all the code:
if ( FCKBrowserInfo.IsGecko )
{
	FCKCommands.RegisterCommand( 'SpellSuggestion', new SpellSuggestion());

	// Activate the spellchecker
	FCKConfig.FirefoxSpellChecker = true ;

	// We need to modify the FCKContextMenu_AttachedElement_OnContextMenu function:
	FCKContextMenu_AttachedElement_OnContextMenu = Inject(FCKContextMenu_AttachedElement_OnContextMenu, 
		function ( ev, fckContextMenu, el )
		{
			// Get the spell suggestions.
			GeckoSpellchecker.fillSuggestions( ev ) ;
		}
		, null ) ;


	// Trick so we get called first and can place the new entries at the top.
	// instead of the call to RegisterListener we directly insert at the first position
	FCK.ContextMenu.Listeners.unshift( {
		AddItems: function( oMenu, oTag, sTagName )
		{
			var suggestions = GeckoSpellchecker.mSpellSuggestions ;

			if ( !suggestions )
				return ;

			// We register a new item for each suggestion, 
			// the suggested replacement 
			// and the spellcheck icon
			// the Tag parameter is the index that will be passed to the Execute command
			for (var i=0; i<suggestions.length; i++ )
				oMenu.AddItem( 'SpellSuggestion', suggestions[i], 13, null, i ) ;

			oMenu.AddSeparator() ;

		}
	} ) ;

	// Done with the setup
}


/**
  @desc  inject the function
  @author  Aimingoo&Riceball
*/
function Inject( aOrgFunc, aBeforeExec, aAtferExec ) {
  return function() {
    if (typeof(aBeforeExec) == 'function') arguments = aBeforeExec.apply(this, arguments) || arguments;
    //convert arguments object to array
    var Result, args = [].slice.call(arguments); 
    args.push(aOrgFunc.apply(this, args));
    if (typeof(aAtferExec) == 'function') Result = aAtferExec.apply(this, args);
    return (typeof(Result) != 'undefined')?Result:args.pop();
  } ;
}
