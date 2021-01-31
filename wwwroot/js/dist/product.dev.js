"use strict";

function _toConsumableArray(arr) { return _arrayWithoutHoles(arr) || _iterableToArray(arr) || _nonIterableSpread(); }

function _nonIterableSpread() { throw new TypeError("Invalid attempt to spread non-iterable instance"); }

function _iterableToArray(iter) { if (Symbol.iterator in Object(iter) || Object.prototype.toString.call(iter) === "[object Arguments]") return Array.from(iter); }

function _arrayWithoutHoles(arr) { if (Array.isArray(arr)) { for (var i = 0, arr2 = new Array(arr.length); i < arr.length; i++) { arr2[i] = arr[i]; } return arr2; } }

$(function () {
  var products = [];
  var temp = [];
  var pages = 0;
  ;
  var itemPages = 16; //========== request to server ========== 

  function reqProducts(type) {
    console.log("Getting Data");
    $.ajax({
      url: '/Product/ProductByCate',
      data: {},
      dataType: 'json',
      method: 'GET'
    }).done(function (response) {
      if (response) renderProducts(response);else showFail(response);
    });
  } //==========  attach event ========== 


  $('#orderby > a').on('click', function () {
    var elm = $(this);
    elm.parent().find('.nav-item.active').removeClass('active');
    elm.addClass('active');
    orderbyProduct(Number(elm.data('index')));
  }); //Filter Data

  onSelectedItemDropdown(filterProduct);

  function onPageChange(callback) {
    $('.page-item').on('click', function () {
      var elm = $(this);
      elm.parent().find('.muted').removeClass('muted');
      elm.addClass('muted');
      callback(Number(elm.data('index')));
      window.scroll({
        top: 500,
        behavior: 'smooth'
      });
    });
  } //========== handler ========== 


  function renderProducts(data) {
    console.log("Data Loaded");
    products = data;
    pagination();
    showProductOnPage(0);
  } //pagination


  function pagination() {
    var len = products.length;
    if (len == 0) return;
    pages = Math.ceil(len / itemPages);
    var container = $('.pagnation');
    container.empty();

    for (var i = 0; i < pages; i++) {
      container.append("<span class=\"btn page-item\" data-index=".concat(i, ">").concat(i + 1, "</span>"));
    }

    console.log("Page is:" + pages);
    onPageChange(showProductOnPage);
  }

  function showProductOnPage(index) {
    console.log("Showing Page: " + index);
    if (index == pages - 1) addProductsElm(products.slice(index * itemPages));else addProductsElm(products.slice(index * itemPages, (index + 1) * itemPages));
  } //render product


  function addProductsElm(items) {
    if (!items || items.length == 0) return;
    var container = $('#product-container').empty();
    items.forEach(function (item) {
      container.append(crtProductElm(item));
    });
    onAddCart();
  }

  function crtProductElm(p) {
      return "<div class=\"col-6 col-lg-3\">\n                    <div class=\"product\" data-itemid=".concat(p.id, ">\n                        <div class=\"product-img img-content\">\n                            <div class=\"img\">\n                                 <img src=\"/img/").concat(p.image, "\">\n                            </div>\n                            <a class=\"add-cart text-center d-none d-lg-block\" href=\"/card">Add to Cart</a>\n                        </div>\n                        <a href=\"/Product/Detail?id=").concat(p.id, "\" class=\"product-content\">\n                            <h4 class=\"pb-2 normal\">").concat(p.name, "</h4>\n                            <div class=\"text-center\">\n                                <p class=\"Rtext-4 black bold d-block d-lg-inline-block m-0\">").concat(cvtIntToMoney(p.price - p.discount), " \u0111</p>\n                                ").concat(p.discount == "0" ? '' : "<del class=\"normal text-sub\">".concat(cvtIntToMoney(p.price), " \u0111</del>"), "\n                            </div>\n                        </a>\n                    </div>\n                </div>");
  } //filter


  function filterProduct(index) {
    if (index == 0) restoreData();else {
      if (temp.length == 0) temp = _toConsumableArray(products);
      renderProducts(products.filter(function (item) {
        return item.wireId = index;
      }));
    }
  }

  function orderbyProduct(index) {
    switch (index) {
      //Popular
      case 0:
        products.sort(function (a, b) {
          return a.saleCount - b.saleCount;
        });
        break;
      //High price

      case 1:
        products.sort(function (a, b) {
          return b.price - b.discount - (a.price - a.discount);
        });
        break;
      //Low price

      case 2:
        products.sort(function (a, b) {
          return a.price - a.discount - (b.price - b.discount);
        });
        break;
    }

    showProductOnPage(0);
  }

  function restoreData() {
    renderProducts(_toConsumableArray(temp));
    temp = [];
  }

  function getCategoryID() {
    var cate = $('#category');
    var idCate = cate.data('category');
    cate.remove();
    return idCate;
  } //==========  end ========== 


  initDropDown();
  reqProducts(getCategoryID());
});