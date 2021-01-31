$(function () {
    let products = [];
    let temp = [];
    let pages = 0;;
    let itemPages = 16;
    /*=======?event======= */
    function onPageChange(callback) {
        $('.page-item').on('click', function () {
            let elm = $(this);
            elm.parent().find('.muted').removeClass('muted');
            elm.addClass('muted')
            callback(Number(elm.data('index')))
            window.scroll({
                top: 500,
                behavior: 'smooth'
            });
        })
    }
    /*=======?handler======= */
    //get code
    function getPageCode() {
        let cate = $('#page');
        let code = cate.data('code');
        cate.remove();
        return code;
    }
    //show data
    function showData(data) {
        $('.pagnation').hide();
        products = data;
        pagination();
        showDataOnPage(0)
    }
    //pagination
    function pagination() {
        let len = products.length;
        let container = $('.pagnation').empty();
        if (len == 0) return;
        pages = Math.ceil(len / itemPages);
        for (let i = 0; i < pages; i++)
            container.append(`<span class="btn page-item" data-index=${i}>${i+1}</span>`)
        onPageChange(showDataOnPage);
    }

    function showDataOnPage(index) {
        UILoader('#product-container');
        if (index == 0) $('.page-item:first-child').addClass('muted');
        if (index == pages - 1) renderProducts(products.slice(index * itemPages), '#product-container')
        else renderProducts(products.slice(index * itemPages, (index + 1) * itemPages), '#product-container');
        $('.pagnation').show();
    }
    //filter
    function filterProduct(index) {
        if (index == 0) {
            showData([...temp])
            temp = [];
        } else {
            if (temp.length == 0) temp = Array.from(products);
            showData(temp.filter(item => item.wireID == index));
        }
    }
    //orderyby
    function orderbyProduct(index) {
        switch (index) {
            //Popular
            case 0:
                products.sort((a, b) => a.saleCount - b.saleCount);
                break;
                //High price
            case 1:
                products.sort((a, b) => (b.price - b.discount) - (a.price - a.discount));
                break;
                //Low price
            case 2:
                products.sort((a, b) => (a.price - a.discount) - (b.price - b.discount));
                break;
        }
        showDataOnPage(0);
    }
    //========== ?exce ========== 
    $("#product-container").on("click", ".add-cart", function () {
        addCart(this, true)
    });
    $('#orderby > a').on('click', function () {
        let elm = $(this);
        elm.parent().find('.nav-item.active').removeClass('active');
        elm.addClass('active');
        orderbyProduct(Number(elm.data('index')));
    });
    reqListProducts(getPageCode(), showData);
    UIDropDown();
    onSelectedItemDropdown(filterProduct);
});