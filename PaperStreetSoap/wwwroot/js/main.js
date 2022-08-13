var postAjax = function (url, formData, callback) {
    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        dataType: 'json',
        encode: true,
        success: function (data) {
            callback(data);
            hideLoader();
        },
        error: function () {
            window.location.href = "/error";
        }
    });
}

var showLoader = function () {
    $('#spinner').addClass('show');
    $('html, body').css({ 'overflow': 'hidden' });
}

var hideLoader = function () {
    $('#spinner').removeClass('show');
    $('html, body').css({ 'overflow': 'unset' });
}

var setDarkModeOnLoad = function () {
    let darkMode = sessionStorage.getItem("dark-mode");

    if (darkMode != null && darkMode == "true") {
        $("#dark-mode-toggle").trigger('click');

        $("body").addClass('dark-mode');
        $('.dark-widget').removeClass('d-none');
        $('.light-widget').addClass('d-none');

        let toggleImage = $('.dark-mode-toggle-label i');

        if (toggleImage.hasClass('fa-sun'))
            toggleImage.removeClass('fa-sun').addClass('fa-moon');
        else
            toggleImage.removeClass('fa-moon').addClass('fa-sun');

        $(".navbar .btn-social img").each(function () {
            let imageSrc = $(this).attr('src');
            $(this).attr('src', imageSrc.replace('dark', 'white'));
        });

        $(".footer .btn-social img").each(function () {
            let imageSrc = $(this).attr('src');
            $(this).attr('src', imageSrc.replace('white', 'dark'));
        });

        let btcPayImageSrc = $('#btc-pay-server-image').attr('src');
        $('#btc-pay-server-image').attr('src', $('#btc-pay-server-image').attr('src').replace('light', 'dark'));
    }
}

var toggleDarkMode = function () {
    if ($('body').hasClass('dark-mode')) {
        $('body').removeClass('dark-mode');
        sessionStorage.setItem("dark-mode", "false");

        $('.dark-widget').addClass('d-none');
        $('.light-widget').removeClass('d-none');

        $(".navbar .btn-social img").each(function () {
            let imageSrc = $(this).attr('src');
            $(this).attr('src', imageSrc.replace('white', 'dark'));
        });

        $(".footer .btn-social img").each(function () {
            let imageSrc = $(this).attr('src');
            $(this).attr('src', imageSrc.replace('dark', 'white'));
        });

        let btcPayImageSrc = $('#btc-pay-server-image').attr('src');
        $('#btc-pay-server-image').attr('src', $('#btc-pay-server-image').attr('src').replace('dark', 'light'));
    }
    else {
        $('body').addClass('dark-mode');
        sessionStorage.setItem("dark-mode", "true");

        $('.dark-widget').removeClass('d-none');
        $('.light-widget').addClass('d-none');

        $(".navbar .btn-social img").each(function () {
            let imageSrc = $(this).attr('src');
            $(this).attr('src', imageSrc.replace('dark', 'white'));
        });

        $(".footer .btn-social img").each(function () {
            let imageSrc = $(this).attr('src');
            $(this).attr('src', imageSrc.replace('white', 'dark'));
        });

        let btcPayImageSrc = $('#btc-pay-server-image').attr('src');
        $('#btc-pay-server-image').attr('src', $('#btc-pay-server-image').attr('src').replace('light', 'dark'));
    }

    let toggleImage = $('.dark-mode-toggle-label i');

    if (toggleImage.hasClass('fa-sun'))
        toggleImage.removeClass('fa-sun').addClass('fa-moon');
    else
        toggleImage.removeClass('fa-moon').addClass('fa-sun');
}

$("document").ready(function () {
    setDarkModeOnLoad();
    showLoader();

    $('a[href]').on('click', function () {
        let href = $(this).attr('href');
        let target = $(this).attr('target')

        if (href != undefined && href.length > 0 && href != "#" && target == undefined)
            showLoader();
    });

    $('button').on('click', function (e) {
        let dataToggleAttribute = $(this).attr('data-bs-toggle');
        let dataDismissAttribute = $(this).attr('data-bs-dismiss');

        let hasAttributes = dataDismissAttribute != undefined || dataToggleAttribute != undefined;

        let isModalButton = $(this).closest('.modal-content').length > 0;

        let isSummerNoteButton = $(this).hasClass('note-btn');

        if (!$(this).hasClass('navbar-toggler') && !hasAttributes && !isSummerNoteButton) {
            let formToValidate = isModalButton ? $(this).closest('.modal-content').find('form') : $(this).closest('form');

            if (formToValidate.length) {
                {
                    $.validator.unobtrusive.parse(formToValidate)
                    if (formToValidate.valid()) {
                        if (formToValidate.hasClass('video-discussion-parent-form')) {
                            e.preventDefault();

                            postAjax(formToValidate.attr('action'), formToValidate.serialize(), function (result) {
                                loadVideoDiscussion(result);
                            });
                        }

                        showLoader();
                    }
                }

            }
            else hideLoader();
        }
        else if (dataToggleAttribute != undefined && dataToggleAttribute != "modal") {
            $('button[data-bs-toggle]').not($(this)).removeClass('btn-primary').addClass('btn-secondary');

            if ($(this).hasClass('btn-primary'))
                $(this).removeClass('btn-primary').addClass('btn-secondary');
            else
                $(this).addClass('btn-primary').removeClass('btn-secondary');
        }
    });


    let windowWidth = $(window).width();

    $(window).on('scroll', function () {
        windowWidth = $(window).width();

        if ($(this).scrollTop() > 100) {
            $('.sticky-top').css('top', '0px');
            $('.sticky-top').addClass('shadow');
        } else {
            $('.sticky-top').css('top', '-100px');
            $('.sticky-top').removeClass('shadow');
        }
    });

    $(window).on('resize', function () {
        windowWidth = $(window).width();

        if (windowWidth < 992) {
            $('.sticky-top').css('top', '0px');
            //$('.sticky-top').addClass('shadow');
        }
        else {
            if ($(window).scrollTop() > 150) {
                $('.sticky-top').css('top', '0px');
                $('.sticky-top').addClass('shadow');
            } else {
                $('.sticky-top').css('top', '-100px');
                $('.sticky-top').removeClass('shadow');
            }
        }
    });

    // Back to top button
    $('.back-to-top').hide();
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 1500, 'easeInOutExpo');
        return false;
    });

    // Dark mode toggler
    $('#dark-mode-toggle, #mobile-dark-mode-toggle').on('change', function () {
        toggleDarkMode();
    });

    $('.navbar-toggler').on('click', function () {
        if ($(this).hasClass('collapsed')) {
            $(this).find('i').addClass('fa fa-bars').removeClass('fa-solid fa-xmark');
        }
        else {
            $(this).find('i').addClass('fa-solid fa-xmark').removeClass('fa fa-bars');
        }
    });

    $(".navbar .btn-social img").hover(function () {
        let imageSrc = $(this).attr('src');
        let darkMode = sessionStorage.getItem("dark-mode");
        let newImageSrc = darkMode != null && darkMode == "true" ? imageSrc.replace('white', 'pink') : imageSrc.replace('dark', 'pink');

        $(this).attr("src", newImageSrc);
    }, function () {
        let imageSrc = $(this).attr('src');
        let darkMode = sessionStorage.getItem("dark-mode");
        let newImageSrc = darkMode != null && darkMode == "true" ? imageSrc.replace('pink', 'white') : imageSrc.replace('pink', 'dark');

        $(this).attr("src", newImageSrc);
    });

    $(".footer .btn-social img").hover(function () {
        let imageSrc = $(this).attr('src');
        let darkMode = sessionStorage.getItem("dark-mode");
        let newImageSrc = darkMode != null && darkMode == "true" ? imageSrc.replace('dark', 'dark') : imageSrc.replace('white', 'pink');

        $(this).attr("src", newImageSrc);
    }, function () {
        let imageSrc = $(this).attr('src');
        let darkMode = sessionStorage.getItem("dark-mode");
        let newImageSrc = darkMode != null && darkMode == "true" ? imageSrc.replace('dark', 'dark') : imageSrc.replace('pink', 'white');

        $(this).attr("src", newImageSrc);
    });

    $('.dropdown-item[data-bs-toggle="sub-dropdown"]').hover(function () {
        $(this).next('.dropdown-submenu').show();
    }, function () {
            $(this).next('.dropdown-submenu').hide();
    });

    $('.dropdown-submenu').hover(function () {
        $(this).closest('.dropdown-menu').css({ 'visibility': 'visible' });
        $(this).css({ 'visibility': 'visible', 'opacity': '1', 'display': 'block' });
    }, function () {
    })
});

$(window).on('load', function () {
    window.setTimeout(function () {
        // Initiate the wowjs
        new WOW(
            {
                mobile: false

            }
        ).init();

        hideLoader();
    }, 300);
});

var loadVideoDiscussion = function (result) {
    if (result != false) {
        let parentId = result.parentId;
        let html = result.html;
        let divContainer = parentId != null ? $('.video-nested-discussion-container[data-id=' + parentId + ']') : $('.video-discussions');

        divContainer.prepend(html);

        if (parentId != null) {
            $("<hr />").insertBefore(divContainer.find('.nested-discussion:first'));

            $('.video-nested-discussion-container[data-id=' + parentId + ']').removeClass('d-none').collapse('show');
            $('.video-nested-discussion-add-reply-container[data-id=' + parentId + ']').collapse('hide');

            let replyCount = divContainer.find('.nested-discussion').length;
            $('.nested-discussion-replies-button[data-id=' + parentId + ']').removeClass('d-none');
            $('.nested-discussion-replies-button[data-id=' + parentId + '] span').text(replyCount + (replyCount == 1 ? ' reply' : ' replies'));
        }
        else {
            $("<hr />").insertBefore(divContainer.find('.parent-discussion:first'));

            let replyButton = divContainer.find('.parent-discussion:first .nested-discussion-reply-button');
            replyButton.remove();
        }

        $('.add-comment-textarea').val('');
    }

}