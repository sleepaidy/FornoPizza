import { useEffect, useState } from 'react'

const copy = {
  ru: {
    sizes: ['Маленькая', 'Средняя', 'Большая'],
    doughs: ['Тонкое', 'Классическое', 'Сырный борт'],
    choosePizza: 'Выберите пиццу',
    chooseLead: 'Соберите позицию и добавьте её в корзину — можно несколько разных.',
    search: 'Поиск',
    searchPlaceholder: 'Имя или состав',
    build: 'Соберите свою',
    pickFirst: 'Сначала выберите пиццу в меню.',
    size: 'Размер',
    dough: 'Тесто',
    toppings: 'Добавки',
    noToppings: 'без добавок',
    add: 'В корзину',
    cart: 'Корзина',
    cartEmpty: 'Пока пусто — соберите пиццу и нажмите «В корзину».',
    checkout: 'Оформление',
    name: 'Имя',
    phone: 'Телефон',
    address: 'Адрес',
    promo: 'Промокод',
    check: 'Проверить',
    promoMissing: 'Промокод не найден или недействителен.',
    promoOk: (discount, finalPrice) => `Код принят, минус ${discount} ₽, итог ${finalPrice} ₽`,
    yourOrder: 'Ваш заказ',
    total: 'Итого',
    serverTotal: 'Итоговую сумму посчитает сервер при оформлении.',
    order: 'Заказать',
    orderNumber: (id) => `Номер заказа: ${id}`,
    missingOne: (item) => `Укажите ${item}.`,
    missingTwo: (first, second) => `Укажите ${first} и ${second}.`,
    missingThree: (first, second, third) => `Укажите ${first}, ${second} и ${third}.`,
    fieldName: 'имя',
    fieldPhone: 'телефон',
    fieldAddress: 'адрес',
  },
  en: {
    sizes: ['Small', 'Medium', 'Large'],
    doughs: ['Thin', 'Classic', 'Cheese crust'],
    choosePizza: 'Choose a pizza',
    chooseLead: 'Build a line and add it to the cart — you can add several different ones.',
    search: 'Search',
    searchPlaceholder: 'Name or ingredients',
    build: 'Build your own',
    pickFirst: 'Choose a pizza from the menu first.',
    size: 'Size',
    dough: 'Dough',
    toppings: 'Toppings',
    noToppings: 'no toppings',
    add: 'Add to cart',
    cart: 'Cart',
    cartEmpty: 'Nothing here yet — build a pizza and press “Add to cart”.',
    checkout: 'Checkout',
    name: 'Name',
    phone: 'Phone',
    address: 'Address',
    promo: 'Promo code',
    check: 'Check',
    promoMissing: 'Promo code was not found or is not valid.',
    promoOk: (discount, finalPrice) => `Code accepted, minus ${discount} ₽, total ${finalPrice} ₽`,
    yourOrder: 'Your order',
    total: 'Total',
    serverTotal: 'The server calculates the total when you place the order.',
    order: 'Place order',
    orderNumber: (id) => `Order number: ${id}`,
    missingOne: (item) => `Enter your ${item}.`,
    missingTwo: (first, second) => `Enter your ${first} and ${second}.`,
    missingThree: (first, second, third) => `Enter your ${first}, ${second}, and ${third}.`,
    fieldName: 'name',
    fieldPhone: 'phone',
    fieldAddress: 'address',
  },
}

function readCulture() {
  const culture = document.getElementById('root')?.dataset.culture
  return culture === 'en' ? 'en' : 'ru'
}

function App() {
  const t = copy[readCulture()]
  const sizes = t.sizes.map((name, id) => ({ id, name }))
  const doughs = t.doughs.map((name, id) => ({ id, name }))
  const [pizzas, setPizzas] = useState([])
  const [toppings, setToppings] = useState([])
  const [chosen, setChosen] = useState(null)
  const [size, setSize] = useState(0)
  const [dough, setDough] = useState(0)
  const [chosenToppingIds, setChosenToppingIds] = useState([])
  const [cart, setCart] = useState([])
  const [nextLineId, setNextLineId] = useState(1)
  const [name, setName] = useState('')
  const [phone, setPhone] = useState('')
  const [address, setAddress] = useState('')
  const [orderId, setOrderId] = useState(null)
  const [query, setQuery] = useState('')
  const [promoCode, setPromoCode] = useState('')
  const [promoError, setPromoError] = useState('')
  const [detailsError, setDetailsError] = useState('')
  const [preview, setPreview] = useState(null)
  const [savedAddresses, setSavedAddresses] = useState([])

  function loadAddresses() {
    fetch('/Account/MyAddresses')
      .then((response) => response.json())
      .then((items) => setSavedAddresses(items))
  }

  useEffect(() => {
    fetch('/Home/Menu')
      .then((response) => response.json())
      .then((menu) => {
        setPizzas(menu.pizzas)
        setToppings(menu.toppings)
      })
    loadAddresses()
  }, [])

  function toggleTopping(id) {
    setChosenToppingIds((current) => {
      if (current.includes(id)) {
        return current.filter((item) => item !== id)
      }
      return [...current, id]
    })
  }

  function orderItems() {
    return cart.map((line) => ({
      pizzaId: line.pizzaId,
      size: line.size,
      dough: line.dough,
      toppingIds: line.toppingIds,
      quantity: line.quantity,
    }))
  }

  function addToCart() {
    const line = {
      id: nextLineId,
      pizzaId: chosen.id,
      pizzaName: chosen.name,
      size,
      sizeName,
      dough,
      doughName,
      toppingIds: [...chosenToppingIds],
      toppingNames,
      quantity: 1,
    }

    setCart((current) => [...current, line])
    setNextLineId(nextLineId + 1)
    setChosen(null)
    setSize(0)
    setDough(0)
    setChosenToppingIds([])
    setPreview(null)
    setPromoError('')
  }

  function readAnswer(response) {
    return response.json().then((data) => {
      if (!response.ok) {
        setPreview(null)
        setPromoError(data.message || t.promoMissing)
        return null
      }

      setPromoError('')
      return data
    })
  }

  function previewPromo() {
    setPromoError('')
    fetch('/Home/PreviewPromo', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        promoCode,
        orderItems: orderItems(),
      }),
    })
      .then(readAnswer)
      .then((result) => {
        if (result) {
          setPreview(result)
        }
      })
  }

  function missingDetailsMessage() {
    const missing = []
    if (!name.trim()) missing.push(t.fieldName)
    if (!phone.trim()) missing.push(t.fieldPhone)
    if (!address.trim()) missing.push(t.fieldAddress)
    if (missing.length === 0) return ''
    if (missing.length === 1) return t.missingOne(missing[0])
    if (missing.length === 2) return t.missingTwo(missing[0], missing[1])
    return t.missingThree(missing[0], missing[1], missing[2])
  }

  function submitOrder() {
    const detailsMessage = missingDetailsMessage()
    if (detailsMessage) {
      setDetailsError(detailsMessage)
      return
    }

    setDetailsError('')
    setPromoError('')
    fetch('/Home/CreateOrderJson', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        clientName: name,
        clientPhone: phone,
        clientAddress: address,
        comment: '',
        promoCode,
        paymentMethod: 0,
        orderItems: orderItems(),
      }),
    })
      .then(readAnswer)
      .then((result) => {
        if (!result) {
          return
        }

        setOrderId(result.orderId)
        setCart([])
        setPreview(null)
        loadAddresses()
      })
  }

  const sizeName = sizes.find((item) => item.id === size).name
  const doughName = doughs.find((item) => item.id === dough).name
  const chosenToppings = toppings.filter((item) => chosenToppingIds.includes(item.id))
  const toppingNames = chosenToppings.map((item) => item.name).join(', ')
  const needle = query.trim().toLowerCase()
  const visiblePizzas = pizzas.filter((pizza) =>
    pizza.name.toLowerCase().includes(needle) ||
    pizza.ingredients.toLowerCase().includes(needle)
  )

  return (
    <div className="order-flow">
      <section className="panel" id="menu">
        <div className="step-head">
          <span className="step-num">01</span>
          <div>
            <h2>{t.choosePizza}</h2>
            <p>{t.chooseLead}</p>
          </div>
        </div>
        <label className="input input--wide menu-search">
          <span>{t.search}</span>
          <input
            value={query}
            onChange={(event) => setQuery(event.target.value)}
            placeholder={t.searchPlaceholder}
          />
        </label>
        <div className="pizza-grid">
          {visiblePizzas.map((pizza) => (
            <button
              key={pizza.id}
              type="button"
              className={chosen?.id === pizza.id ? 'pizza-card is-selected' : 'pizza-card'}
              onClick={() => {
                setChosen(pizza)
                setChosenToppingIds([])
              }}
            >
              <span
                className="pizza-media"
                style={{ backgroundImage: `url(${pizza.imageUrl})` }}
              />
              <span className="pizza-body">
                <span className="pizza-name">{pizza.name}</span>
                <span className="pizza-desc">{pizza.ingredients}</span>
                <span className="pizza-price">{pizza.price} ₽</span>
              </span>
            </button>
          ))}
        </div>
      </section>

      <section className="panel" id="build">
        <div className="step-head">
          <span className="step-num">02</span>
          <div>
            <h2>{t.build}</h2>
            <p>{chosen ? chosen.name : t.pickFirst}</p>
          </div>
        </div>
        <div className="build-grid">
          <fieldset className="field-block">
            <legend>{t.size}</legend>
            <div className="chips">
              {sizes.map((item) => (
                <button
                  key={item.id}
                  type="button"
                  className={size === item.id ? 'chip is-selected' : 'chip'}
                  onClick={() => setSize(item.id)}
                >
                  <span>{item.name}</span>
                </button>
              ))}
            </div>
          </fieldset>
          <fieldset className="field-block">
            <legend>{t.dough}</legend>
            <div className="chips">
              {doughs.map((item) => (
                <button
                  key={item.id}
                  type="button"
                  className={dough === item.id ? 'chip is-selected' : 'chip'}
                  onClick={() => setDough(item.id)}
                >
                  <span>{item.name}</span>
                </button>
              ))}
            </div>
          </fieldset>
          <fieldset className="field-block field-block--wide">
            <legend>{t.toppings}</legend>
            <div className="toppings">
              {toppings.map((item) => (
                <button
                  key={item.id}
                  type="button"
                  className={chosenToppingIds.includes(item.id) ? 'topping is-selected' : 'topping'}
                  onClick={() => toggleTopping(item.id)}
                >
                  {item.name}
                  <em>{item.price} ₽</em>
                </button>
              ))}
            </div>
          </fieldset>
        </div>
        <div className="cart-actions">
          <p className="cart-item__meta">
            {sizeName} · {doughName} · {toppingNames || t.noToppings}
          </p>
          <button className="btn btn-primary" type="button" onClick={addToCart} disabled={!chosen}>
            {t.add}
          </button>
        </div>
      </section>

      <section className="panel" id="cart">
        <div className="step-head">
          <span className="step-num">03</span>
          <div>
            <h2>{t.cart}</h2>
          </div>
        </div>
        {cart.length === 0 && (
          <p className="cart-empty">{t.cartEmpty}</p>
        )}
        <ul className="cart-list">
          {cart.map((line) => (
            <li key={line.id} className="cart-item">
              <div>
                <strong>{line.pizzaName}</strong>
                <p className="cart-item__meta">
                  {line.sizeName} · {line.doughName} · {line.toppingNames || t.noToppings}
                </p>
              </div>
            </li>
          ))}
        </ul>
      </section>

      {cart.length > 0 && (
        <section className="panel checkout" id="order">
          <div>
            <div className="step-head">
              <span className="step-num">04</span>
              <div>
                <h2>{t.checkout}</h2>
              </div>
            </div>
            <div className="form-grid">
              <label className="input">
                <span>{t.name}</span>
                <input
                  value={name}
                  onChange={(event) => {
                    setName(event.target.value)
                    setDetailsError('')
                  }}
                />
              </label>
              <label className="input">
                <span>{t.phone}</span>
                <input
                  value={phone}
                  onChange={(event) => {
                    setPhone(event.target.value)
                    setDetailsError('')
                  }}
                />
              </label>
              <label className="input input--wide">
                <span>{t.address}</span>
                <input
                  value={address}
                  onChange={(event) => {
                    setAddress(event.target.value)
                    setDetailsError('')
                  }}
                />
              </label>
              {savedAddresses.length > 0 && (
                <div className="chips input--wide">
                  {savedAddresses.map((item) => (
                    <button
                      key={item.id}
                      type="button"
                      className={address === item.address ? 'chip is-selected' : 'chip'}
                      onClick={() => {
                        setAddress(item.address)
                        setDetailsError('')
                      }}
                    >
                      <span>{item.address}</span>
                    </button>
                  ))}
                </div>
              )}
              <label className="input input--wide">
                <span>{t.promo}</span>
                <input
                  value={promoCode}
                  onChange={(event) => {
                    setPromoCode(event.target.value)
                    setPromoError('')
                  }}
                />
              </label>
            </div>
            <button className="btn btn-ghost" type="button" onClick={previewPromo}>
              {t.check}
            </button>
            {promoError && <p className="form-error">{promoError}</p>}
            {preview && (
              <p className="cart-item__meta">
                {t.promoOk(preview.discount, preview.finalPrice)}
              </p>
            )}
          </div>
          <aside className="summary">
            <div className="step-head step-head--compact">
              <span className="step-num">05</span>
              <h2>{t.yourOrder}</h2>
            </div>
            <ul className="summary-list">
              {cart.map((line) => (
                <li key={line.id}>
                  <span>
                    {line.pizzaName}, {line.sizeName}, {line.doughName}
                  </span>
                </li>
              ))}
            </ul>
            {preview && (
              <div className="summary-total">
                <span>{t.total}</span>
                <strong>{preview.finalPrice} ₽</strong>
              </div>
            )}
            <p className="summary-note">{t.serverTotal}</p>
            {detailsError && <p className="form-error">{detailsError}</p>}
            <button className="btn btn-primary btn-block" type="button" onClick={submitOrder}>
              {t.order}
            </button>
          </aside>
        </section>
      )}
      {orderId && (
        <section className="panel">
          <p>{t.orderNumber(orderId)}</p>
        </section>
      )}
    </div>
  )
}

export default App