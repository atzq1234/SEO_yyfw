/*
 * RichSpecialChar for FCKeditor
 * FCKeditor ��symbol ���
 * 
 * Developed by  23-Feb-2007
 * �������ڣ�2007��2��23��
 * 
 * ������ã�
 * ȡ��FCKeditor�ٷ�SpecialChar�����ӵ�зǳ��ḻ�ĸ��������ַ����ر��ǶԺ���ƴ����֧�֡�
 * 
 * ����ص㣺
 * �����ԣ�����Ӧ����IE7.0��Firefox2.0������ϣ�֧����Ӣ�ġ��û������޸�symbol.html�ļ����Ի�ø��Ի��汾��
 * 
 * Authors:  ���� (honghuicheng@gmail.com)
 * ���ߣ����� (honghuicheng@gmail.com)
 * 
 * INSTALLATION
 * �����װ
 * 
 * a: unzip the files into your plugin folder.
 * 1����ѹ������ѹ��FCKeditor�Ĳ���ļ����
 *
 * b: add the following to your fckconfig.js.
 * 2�������´�����ӵ�fckconfig.js�ļ��
	FCKConfig.Plugins.Add( 'symbol','zh-cn,en' ) ;
 *
 * c: Then add the 'symbol' item in your toolbar.
 * 3��Ȼ�����symbol���ͼ�굽FCKeditor�������ϡ�
 * Example:
 * Ʃ�磺
	FCKConfig.ToolbarSets["Default"] = [
		['Source','Preview','Templates'],
		['Copy','PasteText'],
		['Undo','Redo','-','Find','Replace','-','SelectAll','RemoveFormat','-','FitWindow'],
		['FontName','FontSize','TextColor','BGColor'],
		'/',
		['Bold','Italic','Underline','StrikeThrough','-','Subscript','Superscript'],
		['OrderedList','UnorderedList','-','Outdent','Indent'],
		['JustifyLeft','JustifyCenter','JustifyRight','JustifyFull'],
		['Link','Unlink'],
		['Image','Flash','Table','Rule','Smiley','symbol','About']
	] ;
 *
 * Good Luck.
 * 
*/

FCKCommands.RegisterCommand( 'symbol', new FCKDialogCommand( 'symbol', FCKLang.SymbolDlgTitle, FCKPlugins.Items['symbol'].Path + 'symbol.html', 480, 360 ) ) ;

var symbolObj = new FCKToolbarButton( 'symbol', FCKLang.SymbolDlgTitle, FCKLang.SymbolDlgTitle, null, null, false, true) ;
symbolObj.IconPath = FCKPlugins.Items['symbol'].Path + 'symbol.gif' ;

FCKToolbarItems.RegisterItem( 'symbol', symbolObj ) ;

