$(function () {
    //status
    $(document).on("click", ".blog-status", function () {
        let blogId = $(this).parent().attr("data-id");
        let changeElem = $(this);
        let data = { id: blogId };

        $.ajax({
            url: "Blog/SetStatus",
            type: "Post",
            data: data,
            success: function (res) {
                if (res) {
                    $(changeElem).removeClass("active-status");
                    $(changeElem).addClass("de-active");
                }
                else {
                    $(changeElem).addClass("active-status")
                    $(changeElem).removeClass("de-active")
                }
            }
        })
    })

    //status
    $(document).on("click", ".slider-status", function () {
        let sliderId = $(this).parent().attr("data-id");
        let changeElem = $(this);
        let data = { id: sliderId };

        $.ajax({
            url: "Slider/SetStatus",
            type: "Post",
            data: data,
            success: function (res) {
                if (res) {
                    $(changeElem).removeClass("active-status");
                    $(changeElem).addClass("de-active");
                }
                else {
                    $(changeElem).addClass("active-status")
                    $(changeElem).removeClass("de-active")
                }
            }
        })
    })

    //remove slider info 
    $(document).on("click", ".delete-item", function () {
        let deleteItem = $(this).parent().parent();
        let sliderInfoId = $(this).parent().attr("data-id");

        let data = { id: sliderInfoId };
        $.ajax({
            url: "sliderinfo/delete",
            type: "Post",
            data: data,
            success: function () {
                $(deleteItem).remove();
                $(".slider-info-table").remove();
                $(".create-item-none").removeClass("d-none")
            }
        })
    })

    //remove slider 
    $(document).on("click", ".delete-slider-btn", function () {
        console.log($(this))
        let deleteItem = $(this).parent().parent();
        let sliderId = $(this).parent().attr("data-id");
        let tbody = $(deleteItem).parent().children()

        let data = { id: sliderId };
        $.ajax({
            url: "slider/delete",
            type: "Post",
            data: data,
            success: function () {
                if ($(tbody).length == 1) {
                    $(".slider-table").remove();
                }
                $(deleteItem).remove();
            }
        })

    })

    //remove subscribe 
    $(document).on("click", ".delete-subs-btn", function () {
        let deleteItem = $(this).parent().parent();
        let subsId = $(this).parent().attr("data-id");

        let data = { id: subsId };
        $.ajax({
            url: "subscribe/delete",
            type: "Post",
            data: data,
            success: function () {
                $(deleteItem).remove();
                $(".subs-table").remove();
                $(".subs-none ").removeClass("d-none")
            }
        })
    })

    //remove blog 
    $(document).on("click", ".delete-blog-item", function () {
        let deleteItem = $(this).parent().parent();
        let blogId = $(this).parent().attr("data-id");
        let tbody = $(deleteItem).parent().children()

        let data = { id: blogId };
        $.ajax({
            url: "blog/delete",
            type: "Post",
            data: data,
            success: function () {
                if ($(tbody).length == 1) {
                    $(".blog-table").remove();
                }
                $(deleteItem).remove();
            }
        })

    })

    //remove category
    $(document).on("click", ".delete-category-btn", function () {
        let deleteItem = $(this).parent().parent();
        let categoryId = $(this).parent().attr("data-id");
        let tbody = $(deleteItem).parent().children()

        let data = { id: categoryId };
        $.ajax({
            url: "category/delete",
            type: "Post",
            data: data,
            success: function () {
                if ($(tbody).length == 1) {
                    $(".category-table").remove();
                    $("nav").removeClass("d-block")
                }
                $(deleteItem).remove();
            }
        })

    })

    //remove expert header info 
    $(document).on("click", ".expertHeader-delete-btn", function () {
        let deleteItem = $(this).parent().parent();
        let expertHeaderInfoId = $(this).parent().attr("data-id");

        let data = { id: expertHeaderInfoId };
        $.ajax({
            url: "expertheader/delete",
            type: "Post",
            data: data,
            success: function () {
                $(deleteItem).remove();
                $(".expertHeader-table").remove();
                $(".create-item-none").removeClass("d-none")
            }
        })
    })

});