var contextMenuItem = {
    "id": "selection",
    "title":"Save to dictionary",
    "contexts":["selection"]
};

chrome.contextMenus.create(contextMenuItem);
chrome.contextMenus.onClicked.addListener(function(clickData){
    if(clickData.menuItemId == 'selection' && clickData.selectionText){
        
    }
});