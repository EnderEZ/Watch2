$(function () {
    "use strict";
    /*=======?gallery======= */
    function Gallery(obj, nav) {
        let gallery = $(`${obj} .img`);
        let nav_gallery = $(nav);
        let widthItemNav = nav_gallery.children(":first-child").width();
        //add offset
        nav_gallery.children().each((idx, elm) => $(elm).attr('offset', idx));
        //handler
        function showImage(img) {
            gallery.css("background-image", `url(${img})`);
        }

        function changeImage(operation) {
            let itemCurr = nav_gallery.children('.active').removeClass("active");
            itemCurr = operation < 0 ? itemCurr.prev() : itemCurr.next();
            if (itemCurr.length == 0)
                itemCurr = operation < 0 ? nav_gallery.children(":last-child") : nav_gallery.children(":first-child");
            itemCurr.addClass('active');
            showImage(itemCurr.children().attr('src'));
            nav_gallery.get(0).scroll({
                left: itemCurr.attr('offset') * widthItemNav,
                behavior: 'smooth'
            });
        }

        nav_gallery.children().on('mouseenter', function () {
            nav_gallery.children('.img.active').removeClass('active');
            showImage($(this).addClass('active').children().attr('src'));
        });

        $(`${obj} .arrow-prev`).on('click', function () {
            console.log("s");
            changeImage(-1);
        });
        $(`${obj} .arrow-next`).on('click', function () {
            changeImage(1);
        });
        changeImage(1);
    }
    Gallery('#gallery', '#gallery-nav');
    /*=======?attach-event======= */
    $('.buy-now').on('click', function () {
        addCart(this, false);
    })
    $('.to-cart').on('click', function () {
        addCart(this, true);
    });
    $("#product-container-other").on("click", ".add-cart", function () {
        addCart(this, true)
    })
    /*=======?exec======= */
    reqListProducts(-2, data => renderProducts(data, '#product-container-other'), 4);
})