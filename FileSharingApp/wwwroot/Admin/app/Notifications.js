
var connection = new signalR.HubConnectionBuilder().withUrl("/Notify").build();

connection.on("RecieveNotifications", function (date) {
    $(".notify").text(moment(date, 'MM-DD-YYYY hh:mm:ss').fromNow());

});
connection.start();





