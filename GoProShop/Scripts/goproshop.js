function openModal(event, element) {
    event.preventDefault();
    return $.get(element.href, function (data) {
        $('#mainDialogContent').html(data);
        $('#mainModal').modal('show');
    });
}

function updateNotificationMessage(element, response) {

    if (response.IsSuccess) {
        $(element).html('<div class="alert alert-success"><i class="fa fa-check-circle fa-lg"></i>  <span>' +
            response.Message +
            '</span></div>').delay(2500).hide(300);
        $(element).show();

    } else {
        $(element).html('<div class="alert alert-danger"><i class="fa fa-exclamation-circle fa-lg"></i>  <span>' +
            response.Message +
            '</span></div>').delay(2500).hide(300);
        $(element).show();
    }
}

function searchProduct(event, element) {
    event.preventDefault();
    var value = $(element).val();
    var searchResult = $('.search-product-box');

    if (value !== "") {
        $.get("/Search/GetProducts",
            { searchString: value },
            function (data) {

                if (data.length) {
                    var list = $('<ul/>')
                        .addClass('search-product-list');

                    $.each(data,
                        function(index, item) {

                            var li = $('<li/>')
                                .appendTo(list);

                            var price = $('<span/>')
                                .addClass('pull-right')
                                .text(item.Price + ' р.');

                            $('<a/>')
                                .attr("href", '/Product/ViewProduct?productId=' + item.Id)
                                .text(item.Name)
                                .prepend(price)
                                .appendTo(li);
                        });

                    $(searchResult).html(list);

                } else {
                    $(searchResult).hide();
                }
            });

        if ($(searchResult)
            .css('display')
            .toLowerCase() !== 'block')
        {
            $(searchResult).show();
        }
       
    } else {
        $(searchResult).hide();
    }
}

function retriveChosenValues(event, id) {
    event.preventDefault();

    var values = $('#product-select').val();

    var data = {
        productsId: values,
        orderId: id
    }

    $.ajaxSettings.traditional = true;
    $.get("/OrderedProduct/UpdateOrderedProducts", data, function (result) {
        if (result.IsSuccess) {
            $('#ordered-products').load(result.Url);
        }
    });

    $('#product-select').val("");

}

function submitModalForm(event) {
    event.preventDefault();

    var form = event.target;
    var formdata = new FormData($(form).get(0));

    $.ajax({
        url: $(form).attr('action'),
        type: $(form).attr('method'),
        data: formdata,
        processData: false,
        contentType: false,
        success: function (result) {
            if (result.IsSuccess) {

                var onSuccess = $(form).data("onSuccess");
                var callback = eval(onSuccess);

                if (typeof callback === 'function') {
                    callback(result);
                }
            }
            else {
                $('#modalBody').html(result);
            }
        }
    });
}

function editFeedbackOnSuccessHandler(data) {
    $("#mainModal").modal('hide');
    $("#admin-feedbacks").load("Feedback/GetAdminFeedbacks/");
    updateNotificationMessage(".table-notification-message", data);
}

function editOrderOnSuccessHandler(data) {
    $('#admin-orders').load("/Order/GetAdminOrders/");
    $('#mainModal').modal('hide');
    updateNotificationMessage(".table-notification-message", data);
}

function productOnSuccessHandler(data) {
    $('#mainModal').modal('hide');
    $("#price-products").load(data.Url);
    updateNotificationMessage(".table-notification-message", data);
};

$('body').on('click', 'a.ajax-get-link', function (e) {
    e.preventDefault();

    var elementToUpdate = $(this).data('elementToUpdate');
    var elementHref = $(this).attr('href');

    $(elementToUpdate).load(elementHref);

});

function editOrder(event, element, elementId) {
    event.preventDefault();

    var spinner = $(element).find('i.fa.fa-spinner');
    var buttonIcon = $(element).find('span.icon-button');

    buttonIcon.hide();
    $(spinner).css("display", "inline-block");

    viewEntity(element, '#new-orders-count', '/Order/View/', elementId);

    $.when(openModal(event, element)).done(function () {
        spinner.hide();
        buttonIcon.show();
    });
};


function viewEntity(element, newEntityCountId, url, elementId) {

    var isViewied = $(element).data("isViewed");
    if (isViewied === "False") {
        $.get(url, { id: elementId }, function (result) {
            $(newEntityCountId).text(result.Count);
        });

        deleteNewEntityLabel(element);
        $(element).data("isViewed", "True");
    }
}


function deleteEntityFromTable(event, element) {
    event.preventDefault();

    return $.get(element.href, function (data) {
        if (data.IsSuccess) {
            element.closest("tr").remove();
        }
    });
}

function editFeedback(event, element, elementId) {
    event.preventDefault();

    viewEntity(element, '#new-feedbacks-count', '/Feedback/View/', elementId);

    openModal(event, element);
};

function deleteNewEntityLabel(element) {
    var parentRow = element.closest("tr");
    var newEntityLabel = $(parentRow).find('td span.label-new-entity');

    newEntityLabel.remove();
}

function deleteProduct(event, element) {
    $.ajaxSetup({ cache: false });
    deleteEntityFromTable(event, element);
}

function deleteFeedback(event, element, elementId) {

    event.preventDefault();
    $.get(element.href, function (data) {
        if (data.IsSuccess) {
            var messageId = $('.table-notification-message');
            viewEntity(element, '#new-feedbacks-count', '/Feedback/View/', elementId);

            $(element).closest("tr").remove();
            updateNotificationMessage(messageId, data);
        }
    });
};

function deleteOrder(event, element, elementId) {
    event.preventDefault();
    $.get(element.href, function (response) {
        if (response.IsSuccess) {

            viewEntity(element, '#new-orders-count', '/Order/View/', elementId);
            var messageId = $('.table-notification-message');

            $(element).closest("tr").remove();
            updateNotificationMessage(messageId, response);
        }
    });
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
    $.get(element.href, function (data) {
        updateOrderInfo();
        shoppingCartBadge.text(data.Quantity);
        $(element).closest("tr").remove();
    });
}

$('.btn-number').click(function (e) {
    e.preventDefault();

    var fieldName = $(this).data('field');
    var type = $(this).data('type');
    var input = $("input[name='" + fieldName + "']");
    var currentVal = parseInt(input.val());
    var cart = $('#shopping-cart span.badge');
    var href = $(this).attr('href');
    var cartItemQuantity = parseInt($(cart).text());

    if (!isNaN(currentVal)) {
        if (type == 'minus') {

            if (currentVal > input.attr('min')) {
                $(".btn-number[data-type='plus'][data-field='" + fieldName + "']").removeAttr('disabled');
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
                $(".btn-number[data-type='minus'][data-field='" + fieldName + "']").removeAttr('disabled');
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

$('.condition-dropdown').change(function (e) {
    var dropdown = $(this);
    var parentRow = dropdown.closest("tr");

    if (dropdown.val() === "1") {
        parentRow.switchClass("table-row-success", "table-row-warning", 300);
    }
    else if (dropdown.val() === "2") {
        parentRow.switchClass("table-row-warning", "table-row-success", 300);
    }
});

$('.nav-tabs').on('click', 'li', function () {
    $('.nav-tabs li.active').removeClass('active');
    $(this).addClass('active');
});

$('.list-group-item').on('click', function (e) {
    $('.list-group').find('.active').removeClass('active');
    $(this).addClass('active');
});
