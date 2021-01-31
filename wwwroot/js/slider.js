function initSlider(container, prev, next, duration = 500, repeat = 0, callBack) {
    let target = $(container);
    let navigator = $(`${prev}, ${next}`);
    let handler = target.find('.slider-handle');
    //root
    let items = target.find('.slider-item').each((idx, obj) => $(obj).attr('offset', idx + 1));
    let widthItem = Math.round(items.width());
    let offSetItems = items.length - 1;
    //cloning
    handler.prepend($(items.get(offSetItems)).clone().addClass('clone').attr('offset', 0));
    handler.append($(items.get(0)).clone().addClass('clone').attr('offset', offSetItems + 2));
    offSetItems += 2;
    //handelr
    function slideTo(item) {
        //callback
        if (callBack) callBack(item);
        //end callback
        let offSet = parseInt(item.attr('offset'));
        //calutate tranlate
        let translateX = -offSet * widthItem;
        //check end or start slide
        let backSlideOffset = -1;
        if (item.hasClass('clone')) {
            backSlideOffset = offSet > 0 ? 1 : offSetItems - 1;
            item = handler.find(".slider-item:nth-child(" + (backSlideOffset + 1) + ")");
        }
        //start-sliding
        navigator.css("pointer-events", "none");
        handler.css({
            "transform": `translateX(${translateX}px)`,
            "transition": `transform ${duration/1000}s ease`
        });
        item.addClass('active');
        //end-sliding
        setTimeout(function () {
            navigator.css("pointer-events", "");
            handler.css('transition', '');
            if (backSlideOffset > 0)
                handler.css("transform", `translateX(${-backSlideOffset*widthItem}px)`);
        }, duration)
    }

    $(navigator.get(0)).on('click', () => {
        slideTo(handler.find('.slider-item.active').removeClass('active').prev());
    })
    $(navigator.get(1)).on('click', () => {
        slideTo(handler.find('.slider-item.active').removeClass('active').next());
    })

    if (repeat != 0) {
        setInterval(function () {
            $(navigator.get(1)).trigger('click');
        }, repeat);
    }
    handler.css("transform", `translateX(-${widthItem}px)`)
    $(".slider-item:nth-child(2)").addClass('active');
}