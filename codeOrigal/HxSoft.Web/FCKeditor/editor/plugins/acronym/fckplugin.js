FCKCommands.RegisterCommand('Acronym', 
  new FCKDialogCommand( 'Acronym', 
    FCKLang.AcronymDlgTitle, 
    FCKPlugins.Items['Acronym'].Path + 'fck_Acronym.html', 
    340, 250 ) ) ;

// Create the "Acronym" toolbar button.
var oAcronymItem = new FCKToolbarButton( 'Acronym', FCKLang.AcronymBtn ) ;
oAcronymItem.IconPath = FCKPlugins.Items['Acronym'].Path + 'Acronym.gif' ;
FCKToolbarItems.RegisterItem( 'Acronym', oAcronymItem ) ;

// The object used for all Acronym operations.
var FCKAcronym = new Object() ;

// Insert a new acronym
FCKAcronym.Insert = function(val){
	var hrefStartHtml = '<acronym Title="'+val+'">';
	var hrefEndHtml = '</acronym>';
	
if (window.getSelection) {  
	mySelection = FCK.EditorWindow.getSelection(); 
}  
else if (document.getSelection){  
	mySelection = FCK.EditorDocument.getSelection(); 
}  
else if (document.selection) {
	mySelection=FCK.EditorDocument.selection.createRange().text;
}
if (mySelection==""){
  mySelection='<sup>[*]</sup>';
  hrefHtml = hrefStartHtml+mySelection+hrefEndHtml+' ';
}else{
  hrefHtml = hrefStartHtml+mySelection+hrefEndHtml;
}
	FCK.InsertHtml(hrefHtml);
	tmpHtml = FCK.GetHTML();
	FCK.SetHTML(tmpHtml,true);
}
