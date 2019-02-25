(function () {
    $(".menu li").on("click", function () {
        $("li .active").removeClass('active');
        $(this).addClass('active');
    });
})();