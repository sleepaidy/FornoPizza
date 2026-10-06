(function () {
    const labels = window.fornoOrderStatusLabels || {
        New: "Новый",
        Confirmed: "Подтверждён",
        Cooking: "Готовится",
        OnTheWay: "В пути",
        Delivered: "Доставлен",
        Canceled: "Отменён"
    };

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/orderHub")
        .withAutomaticReconnect()
        .build();

    connection.on("OrderStatusChanged", function (orderId, status) {
        const card = document.querySelector('article[data-order-id="' + orderId + '"]');
        if (!card) {
            return;
        }

        const badge = card.querySelector(".kitchen-status");
        if (!badge) {
            return;
        }

        badge.textContent = labels[status] || status;
        badge.className = "kitchen-status kitchen-status--" + String(status).toLowerCase();
    });

    connection.start().catch(function (error) {
        console.error("Не удалось подключиться к очереди статусов.", error);
    });
})();
