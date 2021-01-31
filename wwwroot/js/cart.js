$(function () {
    "use strict";
    /*=======?request======= */
    function reqGetCartItems() {
        //Get all id of product in cart
        let itemIDs = undefined; 
        if (order) {
            itemIDs = order.items.reduce((str, currItem) => str += currItem.id + ",", "");
        }
        if (!itemIDs) {
            $('#cart-container')
                .empty()
                .append('<p class="text-center my-5">Пусто</p>');
            return;
        }
        $.ajax({
                url: '/Product/ProductInCart',
                data: {
                    orderItemID: itemIDs
                },
                contentType: "text/plain",
                dataType: 'JSON',
            })
            .done((data) => showData(data))
    }

    function reqProvince() {
        $.ajax({
            url: "/asset/province.json",
            method: "GET",
            dataType: "JSON"
        }).done(resp => showProvince(resp))
    }

    function reqDistrict(id) {
        $.ajax({
            url: `/asset/district/${id}.json`,
            method: "GET",
            dataType: "JSON"
        }).done(resp => showDistrict(resp))
    }
    /*=======?list-cart items======= */
    function updateAmountItem(trigger, operation) {
        let item = $(trigger).parents('.cart-item');
        let countElm = item.find('span[item-count]');
        let priceElm = item.find('span[item-price');
        let count = parseInt(countElm.text()) + (operation ? 1 : -1);
        if (!count) return; // if count == 0 => not thing
        countElm.text(count);
        item.find('span[item-total]').text(
            cvtIntToMoney(cvtMoneyToInt(priceElm) * count)
        ); // update total amount of this item
        changeQuantityOrderItem(item.data('itemid'), operation) // update quantity in order object
        updateAmountPay();
    }

    function updateAmountPay() {
        updateViewCountItem();
        let total = 0;
        let item = $('#cart-container .cart-item');
        item.each(function () {
            total = total + cvtMoneyToInt($(this).find('span[item-total]'));
        });
        $('#bill span[order-total]').text(cvtIntToMoney(total));
        $('#bill span[order-pay]').text(
            cvtIntToMoney(
                total -
                cvtMoneyToInt('#bill span[order-discount]') +
                cvtMoneyToInt('#bill span[order-shipping]')
            )
        );
    }

    function updateViewCountItem() {
        updateViewItemCart();
        let orderItems = $('#cart-items');
        let count = parseInt(orderItems.text());
        $('#count-items').text(count);
        console.log(count);
        if (count == 0)
            $('#cart-container').append('<p class="text-center my-5">Пусто</p>');
    }
    // defien-event
    function attachEventItem() {
        $('.cart-item .btn.add').on('click', function () {
            updateAmountItem(this, true);
        });

        $('.cart-item .btn.subtract').on('click', function () {
            updateAmountItem(this, false);
        });

        $('.cart-item .btn.remove').on('click', function () {
            let item = $(this).parents('.cart-item');
            removeOrderItem(item.data('itemid'));
            item.remove();
            updateAmountPay();
        });
    }
    // treat to Order object
    function isExistOrders() {
        if (!order || order.length == 0) {
            console.log("Order not exsist");
            return false;
        }
        return true;
    }

    function changeQuantityOrderItem(id, operation) {
        if (!isExistOrders()) return;
        let indx = order.items.findIndex(i => i.id == id);
        if (indx < 0) return;
        operation ? order.items[indx].quantity++ : order.items[indx].quantity--;
    }

    function removeOrderItem(id) {
        if (!isExistOrders()) return;
        let indx = order.items.findIndex(i => i.id == id);
        if (indx < 0) return;
        order.items.splice(indx, 1);
    }

    // Render View
    function showData(data) {
        setTimeout(() => {
            renderCartItem(data)
        }, 1000)
    }

    function renderCartItem(items) {
        let container = $('#cart-container').empty();
        if (!items || items.length == 0)
            container.append(`<p class="text-center mt-5 text-3">Пусто</p>`)
        else {
            items.forEach(item => container.append(crtCartItemElm(item)));
            updateAmountPay();
            attachEventItem();
        }
    }

    function crtCartItemElm(p) {
        let orderItem = order.items.find(i => i.id == p.id);
        let priceCurr = p.price - p.discount;
        return `<div class="cart-item mb-3" data-itemid=${p.id}>
                    <div class="row align-items-lg-center">
                        <div class="col-4 col-lg-2">
                            <div class="img">
                                <img src="/products/${p.image}">
                            </div>
                        </div>
                        <div class="col-6 col-lg-8">
                            <div class="row align-items-center">
                                <div class="col-12 col-lg-6 mb-2">
                                    <p>${p.name} - ${p.wireID}</p>
                                    
                                    <span class="normal red text-sub">${cvtIntToMoney(p.price)} грн.</span>
                                </div>
                                <div class="col-12 col-lg-3 mb-2">
                                    <div class="d-flex align-items-center">
                                        <a class="btn btn-sm add"><i class="las la-angle-up"></i></a>
                                        <span item-count class="px-3">${orderItem.quantity}</span>
                                        <a  class="btn btn-sm subtract"><i class="las la-angle-down"></i></a>
                                    </div>
                                </div>
                                <div class="col-12 col-lg-3 mb-2">
                                    <span class="bold">: </span>
                                    <span item-total class="text-4 normal">${cvtIntToMoney(orderItem.quantity * priceCurr)}</span>
                                    <span>Грн</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-2 text-center">
                            <a class="btn red remove" href="#!"><i class=" icon las la-times"></i></i></a>
                        </div>
                    </div>
                </div>`
    }
    /*=======?validate local======= */
    function validateLocal() {
        if (
            checkLocal('provice', 'Chọn Tỉnh/TP') &&
            checkLocal('district', 'Chọn Quận/Huyện')
        ) {
            let local = $('#bill .local').addClass('valid');
            if (local.hasClass('invalid')) local.removeClass('invalid');
            return true;
        }
        return false;
    }

    function checkLocal(target, errorText) {
        let obj = $(`#bill .local input[name="${target}"]`);
        let select = obj.prev();
        let local = obj.parents('.local');
        if (!obj.val()) {
            select.addClass('input-invalid');
            local.addClass('invalid');
            obj.parents('.local').find('.error').text(errorText);
            return false;
        } else {
            if (select.hasClass('input-invalid')) {
                obj.parents('.local').find('.error').text('');
                select.removeClass('input-invalid');
            }
            return true;
        }
    }
    /*=======?local data======= */
    function showProvince(province) {
        renderLocal('#bill .local div[local-provice] .dropdown-menu', province);
        $('#bill .control-input div[local-district]').addClass('muted')
        onSelectedItemDropdown(reqDistrict)
    }

    function showDistrict(district) {
        renderLocal('#bill .local  div[local-district] .dropdown-menu', district)
        $('#bill .control-input div[local-district]').removeClass('muted');
        onSelectedItemDropdown()
        validateLocal();
    }
    //render local
    function renderLocal(target, data) {
        if (typeof target == 'string') target = $(target);
        Object.values(data).forEach(item =>
            target.append(`<li data-code="${item.code}">${item.name_with_type}</li>`)
        );
    }
    /*=======?attach event from======= */
    $('#bill .btn').on('click', () => $('#form button').trigger('click'));
    //begin validate
    $('#form input').on('click', () => {
        $('#form').find('.validate > input').addClass('input-invalid')
        validateLocal();
    });
    //begin validate for dropdown
    $('#form .local').on('mouseout', function () {
        validateLocal();
        $(this).find('.error').parent().hide();
    });
    $('#form .local').on('mouseover', function () {
        $(this).find('.error').parent().show();
    });
    /*=======?exec======= */
    UIDropDown();
    reqProvince();
    reqGetCartItems();
});