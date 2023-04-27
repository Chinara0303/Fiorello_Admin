$(document).ready(function () {

    //calculate grand and sub total
    function grandTotal() {
        let tbody = $(".tbody").children()
        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(4).children().eq(1).text())
            sum += price
        }
        $(".grand-total").text(sum + ".00");
    }
    function subTotal(res, nativePrice, total, count) {
        $(count).text(res);
        let subtotal = nativePrice * parseFloat($(count).text());
        $(total).text(subtotal + ".00");
    }

    //check cookie
    checkCookie()
    function checkCookie() {
        const cookieValue = decodeURIComponent(document.cookie.split('=')[1])

        if (cookieValue == "[]" || !document.cookie.includes("basket") ) {
            $(".footer-alert").removeClass("d-none");
        }
        else {
            $(".footer-alert").addClass("d-none");
        }
    }

    //show more function
    $(document).on("click", ".show-more", function () {
        let skipCount = $(".parent-products").children().length;
        let dataCount = $(".parent-products").attr("data-count");
        $.ajax({
            url: `shop/loadmore?skip=${skipCount}`,
            type: "Get",
            success: function (res) {
                $(".parent-products").append(res);

                skipCount = $(".parent-products").children().length;

                if (skipCount >= dataCount) {
                    $(".show-more").addClass("d-none");
                    $(".show-less").removeClass("d-none");
                }
            }
        })
    })

    //show less function
    $(document).on("click", ".show-less", function () {
        let skipCount = 0;

        $.ajax({
            url: `shop/loadmore?skip=${skipCount}`,
            type: "Get",
            success: function (res) {
                $(".parent-products").html("")
                $(".parent-products").append(res);


                $(".show-more").removeClass("d-none");
                $(".show-less").addClass("d-none");
            }
        })
    })

    //add product to basket in home page
    $(document).on("submit", ".price form", function () {
        let id = $(this).attr("data-id");
        $.ajax({
            type: "Post",
            url: `home/addbasket?id=${id}`,
            success: function (res) {
                $(".rounded-circle").text(res);

                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Product added to basket',
                    showConfirmButton: false,
                    timer: 1000
                })
            }
        })
        return false;
    })

    //add product to basket in shop page
    $(document).on("submit", ".button form", function () {
        let id = $(this).attr("data-id");
        $.ajax({
            type: "Post",
            url: `shop/addbasket?id=${id}`,
            success: function (res) {
                $(".rounded-circle").text(res)
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Product added to basket',
                    showConfirmButton: false,
                    timer: 1000
                })
            }
        })
        return false;
    })

    //delete product from basket
    $(document).on("click", ".delete-product", function () {
        
        let id = $(this).parent().parent().attr("data-id");
        let prod = $(this).parent().parent();
        let tbody = $(".tbody").children();

        $.ajax({
            type: "Get",
            url: `Basket/DeleteDataFromBasket?id=${id}`,
            success: function (res) {
                Swal.fire({
                    title: 'Are you sure?',
                    text: "You won't be able to revert this!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, delete it!'
                }).then((result) => {
                    if (result.isConfirmed) {
                        if ($(tbody).length == 1) {
                            $(".product-table").addClass("d-none");
                            $(".footer-alert").removeClass("d-none")
                        }
                        $(prod).remove();
                        $(".rounded-circle").text(res)
                        grandTotal();
                        Swal.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        )
                    }
                })
               
            }
        })
    })

    //change product count
    $(document).on("click", ".increment", function () {
        let id = $(this).parent().parent().parent().attr("data-id");
        let nativePrice = parseFloat($(this).parent().parent().prev().children().eq(1).text());
        let total = $(this).parent().parent().next().children().eq(1);
        let count = $(this).prev();

        $.ajax({
            type: "Get",
            url: `Basket/IncrementProductCount?id=${id}`,
            success: function (res) {
                res++;
                subTotal(res, nativePrice, total, count)
                grandTotal();
            }
        })
    })

    $(document).on("click", ".decrement", function () {
        let id = $(this).parent().parent().parent().attr("data-id");
        let nativePrice = parseFloat($(this).parent().parent().prev().children().eq(1).text());
        let total = $(this).parent().parent().next().children().eq(1);
        let count = $(this).next();

        $.ajax({
            type: "Get",
            url: `Basket/DecrementProductCount?id=${id}`,
            success: function (res) {
                if ($(count).text() == 1) {
                    return;
                }
                res--;
                subTotal(res,nativePrice,total,count)
                grandTotal();
            }
        })
    })

    //search
    $(document).on("keyup", "#input-search", function () {
        let value = $("#input-search").val();
        $("#search-list li").slice(1).remove();
        $.ajax({
            type: "Get",
            url: `shop/search?searchText=${value}`,
            success: function (res) {
                $("#search-list").append(res)
            }
        })
    })


    // HEADER
    $(document).on('click', '#search', function () {
        $(this).next().toggle();
    })

    $(document).on('click', '#mobile-navbar-close', function () {
        $(this).parent().removeClass("active");

    })
    $(document).on('click', '#mobile-navbar-show', function () {
        $('.mobile-navbar').addClass("active");

    })

    $(document).on('click', '.mobile-navbar ul li a', function () {
        if ($(this).children('i').hasClass('fa-caret-right')) {
            $(this).children('i').removeClass('fa-caret-right').addClass('fa-sort-down')
        }
        else {
            $(this).children('i').removeClass('fa-sort-down').addClass('fa-caret-right')
        }
        $(this).parent().next().slideToggle();
    })

    // SLIDER

    $(document).ready(function(){
        $(".slider").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });

    // PRODUCT

    $(document).on('click', '.categories', function(e)
    {
        e.preventDefault();
        $(this).next().next().slideToggle();
    })

    $(document).on('click', '.category li a', function (e) {
        e.preventDefault();
        let category = $(this).attr('data-id');
        let products = $('.product-item');
        
        products.each(function () {
            if(category == $(this).attr('data-id'))
            {
                $(this).parent().fadeIn();
            }
            else
            {
                $(this).parent().hide();
            }
        })
        if(category == 'all')
        {
            products.parent().fadeIn();
        }
    })

    // ACCORDION 

    $(document).on('click', '.question', function()
    {   
       $(this).siblings('.question').children('i').removeClass('fa-minus').addClass('fa-plus');
       $(this).siblings('.answer').not($(this).next()).slideUp();
       $(this).children('i').toggleClass('fa-plus').toggleClass('fa-minus');
       $(this).next().slideToggle();
       $(this).siblings('.active').removeClass('active');
       $(this).toggleClass('active');
    })

    // TAB

    $(document).on('click', 'ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().next().children('p.active').removeClass('active');

        $(this).parent().next().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    $(document).on('click', '.tab4 ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().parent().next().children().children('p.active').removeClass('active');

        $(this).parent().parent().next().children().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    // INSTAGRAM

    $(document).ready(function(){
        $(".instagram").owlCarousel(
            {
                items: 4,
                loop: true,
                autoplay: true,
                responsive:{
                    0:{
                        items:1
                    },
                    576:{
                        items:2
                    },
                    768:{
                        items:3
                    },
                    992:{
                        items:4
                    }
                }
            }
        );
      });

      $(document).ready(function(){
        $(".say").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });
})