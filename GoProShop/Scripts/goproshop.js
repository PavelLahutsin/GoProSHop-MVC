function openModal(event, element) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, function (data) {
        $('#mainDialogContent').html(data);
        $('#mainModal').modal('show');
    });
}

function updateResponseMessage(element, response) {

    if (response.IsSuccess) {
        $(element).html('<div class="alert alert-success"><i class="fa fa-check-circle fa-lg"></i>  <span>' +
            response.Message +
            '</span></div>');

    } else {
        $(element).html('<div class="alert alert-danger"><i class="fa fa-exclamation-circle fa-lg"></i>  <span>' +
            response.Message +
            '</span></div>');
    }
}

function getMethodClickHandler(event, element, elementToUpdate) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, function (data) {
        $(elementToUpdate).html(data);
    });
}

function loginClickHandler(event, element) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, function (data) {
        $('#dialogContent').html(data);
        $('#loginModal').modal('show');
    });
}

function editFeedback(event, element, elementId) {
    event.preventDefault();
    $.get('/Feedback/View/', { id: elementId }, function (result) {
        $('#pending-feedback-count').text(result.Count);
        $.get(element.href, function (data) {
            $('#mainDialogContent').html(data);
            $('#mainModal').modal('show');
        });
    });
}

function deleteProduct(event, element) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, function (data) {
        $('#products').html(data);
    });
}

function deleteFeedback(event, element, elementId) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, function (data) {
        if (data.IsSuccess) {
            $.get('/Feedback/View/', { id: elementId }, function (dataResult) {
                $('#pending-feedback-count').text(dataResult.Count);
            });
            $('#admin-feedbacks').load("Feedback/GetAdminFeedbacks");

            updateResponseMessage("#admin-feedbacks-message", data);
        }
    });
};


function updateFeedbacks(data) {
    if (data.IsSuccess) {
        updateAlertMessage(data.Message);
        $.get('Feedback/GetAdminFeedbacks', function (feedbackData) {
            $('#admin-feedbacks').html(feedbackData);
        });
        $("#mainModal").modal('hide');
    }
}

function unloadModal(data) {
    if (data.success) {
        $('#loginModal').modal('hide');
        window.location.reload(true);
    }
};

if ($('#scrollToTop').length) {
    var scrollTrigger = 300, // px
        backToTop = function () {
            var scrollTop = $(window).scrollTop();
            if (scrollTop > scrollTrigger) {
                $('#scrollToTop').addClass('show');
            } else {
                $('#scrollToTop').removeClass('show');
            }
        };
    backToTop();
    $(window).on('scroll', function () {
        backToTop();
    });
    $('#scrollToTop').on('click', function (e) {
        e.preventDefault();
        scrollToTop();
    });
}

function scrollToTop() {
    $('html,body').animate({
        scrollTop: 0
    }, 700);
}

function CRate(r) {
    $("#Rating").val(r);
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).removeClass('fa-star-o');
        $("#Rate" + i).addClass('fa-star fa-star-selected');
    }
    for (var i = r + 1; i <= 5; i++) {
        $("#Rate" + i).removeClass('fa-star fa-star-selected');
        $("#Rate" + i).addClass('fa-star-o');
    }
}

function CRateOver(r) {
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).removeClass('fa-star-o');
        $("#Rate" + i).addClass('fa-star fa-star-selected');
    }
}

function CRateOut(r) {
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).removeClass('fa-star fa-star-selected');
        $("#Rate" + i).addClass('fa-star-o');
    }
}

function CRateSelected() {
    var setRating = $("#Rating").val();
    for (var i = 1; i <= setRating; i++) {
        $("#Rate" + i).removeClass('fa-star-o');
        $("#Rate" + i).addClass('fa-star fa-star-selected');
    }
}


function adminTabClickHandler(event, element) {
    var $this = $(this);
    $this.addClass('active');
    event.preventDefault();
    $.get(element.href, function (data) {
        $('#adminContent').html(data);
    });
};

function productTabClickHandler(event, element, elementId) {
    event.preventDefault();

    var $this = $(this);

    $this.addClass('active');
    $.get(element.href, { productId: elementId }, function (partialView) {
        $('#view-product-content').html(partialView);
    });
};

function addToCart(event, element) {
    event.preventDefault();

    var spinner = $(element).find(".fa-spinner");
    spinner.css("display", "inline-block");

    $.get(element.href, function (data) {
        spinner.hide();
        $('#shopping-cart span.badge').text(data.Quantity);
    });
}

function deleteCartItemModal(event, element) {

    var shoppingCartBadge = $('#shopping-cart span.badge');
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, function (data) {
        $('#mainDialogContent').load("/Cart/CartView");
        shoppingCartBadge.text(data.Quantity);

    });
}

function deleteCartItem(event, element) {
    event.preventDefault();

    var shoppingCartBadge = $('#shopping-cart span.badge');
    var parentRow = $(element).closest("tr");
    $.get(element.href, function (data) {
        updateOrderInfo();
        shoppingCartBadge.text(data.Quantity);
        parentRow.remove();

    });
}

$('.btn-number').click(function (e) {
    e.preventDefault();

    var fieldName = $(this).attr('data-field');
    var type = $(this).attr('data-type');
    var input = $("input[name='" + fieldName + "']");
    var currentVal = parseInt(input.val());
    var cart = $('#shopping-cart span.badge');
    var href = $(this).attr('href');
    var cartItemQuantity = parseInt($(cart).text());

    if (!isNaN(currentVal)) {
        if (type == 'minus') {

            if (currentVal > input.attr('min')) {
                $(".btn-number[data-type='plus'][data-field='" + fieldName + "']").removeAttr('disabled')
                input.val(currentVal - 1).change();
                $.get(href, function (data) {
                    if (data.IsSuccess) {
                        $(cart).text(cartItemQuantity - 1);
                        updateOrderInfo();
                    }
                });
            }
            if (parseInt(input.val()) == input.attr('min')) {
                $(this).attr('disabled', true);
            }

        } else if (type == 'plus') {

            if (currentVal < input.attr('max')) {
                $(".btn-number[data-type='minus'][data-field='" + fieldName + "']").removeAttr('disabled')
                input.val(currentVal + 1).change();
                $.get(href, function (data) {
                    if (data.IsSuccess) {
                        $(cart).text(cartItemQuantity + 1);
                        updateOrderInfo();
                    }
                });
            }
            if (parseInt(input.val()) == input.attr('max')) {
                $(this).attr('disabled', true);
            }

        }
    } else {
        input.val(0);
    }
});


function updateOrderInfo() {
    $("#order-info").load("/Cart/OrderInfo/");
}

$('.nav-tabs').on('click', 'li', function () {
    $('.nav-tabs li.active').removeClass('active');
    $(this).addClass('active');
});

$('.list-group-item').on('click', function (e) {
    $('.list-group').find('.active').removeClass('active');
    $(this).addClass('active');
});
