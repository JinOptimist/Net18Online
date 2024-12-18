$(document).ready(function(){
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/hub/alertGamePage")
        .build();

    hub.on("Alert", function (message) {
            console.log("Получено сообщение:", message);
            showNotification(message); 
    });
        

    hub
    .start()
    .then(() => {
        hub.invoke('userEnteredToSite');            
    });    

    function showNotification(message, duration = 3000) {
        const notification = document.getElementById('alert');
        
        
        notification.querySelector('p').textContent = message;
    
        
        notification.classList.remove('hidden');
        notification.classList.add('visible');
    
        
        setTimeout(() => {
            notification.classList.remove('visible');
            notification.classList.add('hidden');
        }, duration);
    }        
});
