var order
/*=======?request======= */
function reqListProducts(pageCode, callBack, number) {
	$.ajax({
		url: "/Product/ProductByCate",
		data: {
			pageCode: pageCode,
			number: number,
		},
		dataType: "json",
		method: "GET",
	}).done((response) => {
		console.log(response)
		callBack(response)
	})
}
/*=======?logging======= */
$(document).ajaxComplete(function (event, xhr, opts) {
	console.log(event.type + " - " + opts.url + " - " + xhr.status)
})
/*=======?ui-event======= */
function onSelectedItemDropdown(callBack) {
	$(".dropdown .dropdown-menu li").on("click", function () {
		let item = $(this)
		let dropdown = item.parents(".dropdown")
		let index = Number(item.data("code"))
		dropdown.find("input").attr("value", index)
		dropdown
			.find(".select span")
			.removeClass("placeholder")
			.text(item.text())
		if (callBack) callBack(index)
	})
}

function addCart(elm, popup) {
	let id = $(elm).parents(".product").data("itemid")
	if (!id) id = $(elm).parents(".product-text").data("itemid")
	if (typeof id == "undefined") window.location.href = "/error"
	addItemToCart(id)
	updateViewItemCart()
	if (!popup) return
	UIPopup(
		"Дабавлено в корзину",
		"Просмотр корзины",
		"Дальше",
		"/card"
	)
}
/*=======?ui-elemetn======= */
function UIPopup(
	message,
	resloveName,
	rejectName,
	redirectPage,
	rejectCallback
) {
	let popup = $(`<div class="popup show-popup">
						<div class="popup-content">
							<p>${message}</p>
							<div class="popup-control">
								<a control-resolve class="btn btn-sm bg-red white" href="#!">${resloveName}</a>
								<a control-reject class="btn btn-sm bg-black white" href="#!">${rejectName}</a>
							</div>
						</div>
					</div>`)
	popup.find("a[control-resolve]").on("click", function () {
		window.location.href = redirectPage
		close()
	})
	popup.find("a[control-reject]").on("click", function () {
		rejectCallback
		close()
	})

	function close() {
		// $('body').removeClass('no-overflow');
		popup.removeClass("show-popup").addClass("hide-popup")
		setTimeout(() => popup.remove(), 200)
	}
	$("body").append(popup)
}

function UIDropDown() {
	$(".dropdown").click(function () {
		$(this).attr("tabindex", 1).focus()
		$(this).toggleClass("active")
		$(this).find(".dropdown-menu").slideToggle(300)
	})
	$(".dropdown").focusout(function () {
		$(this).removeClass("active")
		$(this).find(".dropdown-menu").slideUp(300)
	})
}

function UILoader(target) {
	$(target)
		.empty()
		.append(
			'</div><div></div><div></div><div></div></div>'
		)
}
/*=======?render product======= */
function renderProducts(items, target) {
	setTimeout(function () {
		let container = $(target).empty()
		if (!items || items.length == 0)
			container
			.removeClass("row")
			.append(
				`<p class="text-center mt-5 text-4">Không có sản phẩm phù hợp</p>`
			)
		else {
			container.addClass("row")
			items.forEach((item) => container.append(crtProductElm(item)))
		}
	}, 600)
}

function crtProductElm(p) {
	return `<div class="col-6 col-lg-3">
                    <div class="product data-itemid=${p.id}>
                        <div class="product-img img-content">
                            <div class="img">
                                 <img src="/products/${p.image}" loading="lazy">
                            </div>
                        </div>
                        <a style="text-align: center; display: block !important;" href="/san-pham/?id=${p.id}" class="product - content">
                            <h4 class="pb-2 normal">${p.name} - ${p.wireID}</h4>
                            <div class="text-center">
                                <p class="text-4 black bold d-block d-lg-inline-block m-0">${cvtIntToMoney(
									p.price - p.discount
								)} Грн.</p>
    
                            </div>
                        </a>
                    </div>
                </div>`
}
/*=======?helpful======= */
function cvtMoneyToInt(target) {
	if (typeof target == "string") target = $(target)
	return parseInt(target.text().split(",").join(""))
}

function cvtIntToMoney(val) {
	return new Intl.NumberFormat().format(val)
}

function addItemToCart(id) {
	let index = order.items.findIndex((item) => item.id == id)
	if (index >= 0) order.items[index].quantity++
	else
		order.items.push({
			id: id,
			quantity: 1,
		})
}

function visibleElement(eOpen, eClose, eTarget) {
	try {
		eOpen.on("click", () =>
			eTarget.removeClass("hide-item").addClass("show-item")
		)
		eClose.on("click", () =>
			eTarget.removeClass("show-item").addClass("hide-item")
		)
	} catch {
		throw "Not is JQuery Element"
	}
}

function updateViewItemCart() {
	$("#cart-items").text(
		order.items.reduce((t, item) => (t = t + item.quantity), 0)
	)
}
/*=======?exec======= */
$(function () {
	visibleElement($("#menu"), $(".nav-mobile .btn-classic"), $(".nav-mobile"))
	visibleElement($("#search"), $(".searchbar .btn-classic"), $(".searchbar"))
	$("#search-sm").on("click", () => {
		$(".nav-mobile .btn-classic").click()
		$("#search").click()
	})
	$(".search-input input").on("keydown", function (e) {
		if (e.keyCode == 13) $(this).parents("form").trigger("submit")
	})
})