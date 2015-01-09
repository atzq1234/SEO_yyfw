//注册命令的方法是FCKCommands.RegisterCommand（命令名称，对话框命令）
//创建对话框命令的格式：new FCKDialogCommand( 命令名称, 对话框标题,url路径, 宽度,高度) 

FCKCommands.RegisterCommand( 'Insertplayer', new FCKDialogCommand( 'Insertplayer', FCKLang["InsertplayerBtn"],  
FCKPlugins.Items['Insertplayer'].Path + 'fck_Insertplayer.html', 340, 200 ) ) ; 

// 创建工具栏按钮 new FCKToolbarButton( 按钮名称, 按钮标题 ) ;
var oInsertplayerItem = new FCKToolbarButton( 'Insertplayer', FCKLang["InsertplayerBtn"] ) ; 
oInsertplayerItem.IconPath = FCKPlugins.Items['Insertplayer'].Path + 'Insertplayer.gif' ; 
FCKToolbarItems.RegisterItem( 'Insertplayer', oInsertplayerItem ) ; 

//创建用于所有Insertplayer操作的对象 
var FCKInsertplayer = new Object() ; 

//在当前的选择上插入一个超级链接
// 这个添加的方法将在弹出窗口点击ok按钮时被调用。
// 该方法将会接收从对话框中传来的值。
var txtID = parseInt(Math.random()*7999)

FCKInsertplayer.Add = function( videourl, videoimg ) { 

	if(videourl.substr(0,4) != "http" && videourl.substr(0,4) != "HTTP") 
	videourl = "http://"+videourl ;
	if(videoimg.substr(0,4) != "http" && videoimg.substr(0,4) != "HTTP") 
	videoimg = "http://"+videoimg ;
	playerurl = FCKPlugins.Items['Insertplayer'].Path + 'player.swf' ;

	FCK.InsertHtml("<div class=video><embed src="+playerurl+" width=100% height=100% bgcolor=undefined allowscriptaccess=always allowfullscreen=true flashvars=file="+videourl+"&backcolor=232323&frontcolor=EFEFEF&lightcolor=66CC00&screencolor=000000&controlbar=over&showdownload=true&image="+videoimg+" /></div>") ; 
}