(function () {
    const form = document.querySelector(".order-flow");
    const addBtn = document.getElementById("add-to-cart");
    const cartFields = document.getElementById("cart-fields");
    const cartList = document.getElementById("cart-list");
    const cartEmpty = document.getElementById("cart-empty");
    const cartSummary = document.getElementById("cart-summary");

    if (!form || !addBtn || !cartFields || !cartList || !cartEmpty || !cartSummary) {
        return;
    }

    const sizeLabels = {
        Small: "25 см",
        Medium: "30 см",
        Big: "35 см"
    };

    const doughLabels = {
        Thin: "Тонкое",
        Classic: "Классическое",
        CheeseFilledCrust: "С сырным бортом"
    };

    /** @type {{ pizzaId: string, size: string, dough: string, toppingIds: string[], quantity: string }[]} */
    let items = [];

    function labelFromDraft(name, value, fallback) {
        const el = form.querySelector(`[name="${name}"][value="${value}"]`);
        return (el && el.dataset.name) || fallback;
    }

    function selectedValue(name) {
        const el = form.querySelector(`[name="${name}"]:checked`);
        return el ? el.value : null;
    }

    function selectedValues(name) {
        return Array.from(form.querySelectorAll(`[name="${name}"]:checked`)).map((el) => el.value);
    }

    function readDraft() {
        const pizzaId = selectedValue("draft.PizzaId");
        const size = selectedValue("draft.Size");
        const dough = selectedValue("draft.Dough");
        const quantity = selectedValue("draft.Quantity");
        const toppingIds = selectedValues("draft.ToppingIds");

        if (!pizzaId || !size || !dough || !quantity) {
            return null;
        }

        return { pizzaId, size, dough, toppingIds, quantity };
    }

    function formatItem(item) {
        const pizza = labelFromDraft("draft.PizzaId", item.pizzaId, "Пицца #" + item.pizzaId);
        const size = sizeLabels[item.size] || item.size;
        const dough = doughLabels[item.dough] || item.dough;
        const toppings = item.toppingIds.length
            ? item.toppingIds.map((id) => labelFromDraft("draft.ToppingIds", id, "#" + id)).join(", ")
            : "без добавок";

        return {
            title: pizza + " × " + item.quantity,
            meta: size + " · " + dough + " · " + toppings
        };
    }

    function createHiddenInput(name, value) {
        const input = document.createElement("input");
        input.type = "hidden";
        input.name = name;
        input.value = value;
        return input;
    }

    function renderHiddenFields() {
        cartFields.replaceChildren();

        items.forEach((item, index) => {
            const prefix = "OrderItems[" + index + "]";
            cartFields.appendChild(createHiddenInput(prefix + ".PizzaId", item.pizzaId));
            cartFields.appendChild(createHiddenInput(prefix + ".Size", item.size));
            cartFields.appendChild(createHiddenInput(prefix + ".Dough", item.dough));
            cartFields.appendChild(createHiddenInput(prefix + ".Quantity", item.quantity));
            item.toppingIds.forEach((toppingId) => {
                cartFields.appendChild(createHiddenInput(prefix + ".ToppingIds", toppingId));
            });
        });
    }

    function renderLists() {
        cartList.replaceChildren();
        cartSummary.replaceChildren();

        const hasItems = items.length > 0;
        cartEmpty.hidden = hasItems;

        items.forEach((item, index) => {
            const text = formatItem(item);

            const li = document.createElement("li");
            li.className = "cart-item";

            const info = document.createElement("div");
            info.className = "cart-item__info";

            const title = document.createElement("strong");
            title.textContent = text.title;

            const meta = document.createElement("div");
            meta.className = "cart-item__meta";
            meta.textContent = text.meta;

            info.appendChild(title);
            info.appendChild(meta);

            const removeBtn = document.createElement("button");
            removeBtn.type = "button";
            removeBtn.className = "cart-item__remove";
            removeBtn.textContent = "Удалить";
            removeBtn.addEventListener("click", function () {
                items.splice(index, 1);
                render();
            });

            li.appendChild(info);
            li.appendChild(removeBtn);
            cartList.appendChild(li);

            const summaryLi = document.createElement("li");
            const summaryTitle = document.createElement("span");
            summaryTitle.textContent = text.title;
            const summaryMeta = document.createElement("strong");
            summaryMeta.textContent = text.meta;
            summaryLi.appendChild(summaryTitle);
            summaryLi.appendChild(summaryMeta);
            cartSummary.appendChild(summaryLi);
        });
    }

    function render() {
        renderHiddenFields();
        renderLists();
    }

    function resetDraftToppings() {
        form.querySelectorAll('[name="draft.ToppingIds"]:checked').forEach((el) => {
            el.checked = false;
        });
    }

    addBtn.addEventListener("click", function () {
        const draft = readDraft();
        if (!draft) {
            alert("Выберите пиццу, размер, тесто и количество.");
            return;
        }

        items.push(draft);
        resetDraftToppings();
        render();
    });

    form.addEventListener("submit", function (event) {
        if (items.length === 0) {
            event.preventDefault();
            alert("Добавьте хотя бы одну позицию в корзину.");
        }
    });

    render();
})();
