function mainModalClickCartHandler(element, event) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, function (data) {
        $('#mainDialogContent').html(data);
        $('#mainModal').modal('show');
    });
}

function mainModalClickButtonHandler(event) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get('/Product/Create', function (data) {
        $('#mainDialogContent').html(data);
        $('#mainModal').modal('show');
    });
}

function viewFeedbackClickHandler(event, element, elementId) {
    event.preventDefault();
    $.get('/Feedback/View/', { id: elementId }, function (result) {
        $('#pending-feedback-count').html(result.Count);
        $.get(element.href, { id: elementId }, function (data) {
            $('#mainDialogContent').html(data);
            $('#mainModal').modal('show');
        });
    });
}

function mainModalClickHandler(event, element, elementId) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, { id: elementId }, function (data) {
        $('#mainDialogContent').html(data);
        $('#mainModal').modal('show');
    });
}
function deleteItemClickHandler(event, element, elementId) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, { id: elementId }, function (data) {
        var alertElement;
        if (data.IsSuccess) {
            $.get('/Feedback/View/', { id: elementId }, function (dataResult) {
                $('#pending-feedback-count').html(dataResult.Count);
            });
            updateAlertMessage(data.Message);
            $.get('Feedback/GetAdminFeedbacks', function (dataResult) {
                $('#admin-feedbacks').html(dataResult);
            });
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

function updateAlertMessage(message) {
    var result = '<div class="alert alert-success alert-dismissable fade in"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + message + '</strong></div>';
    $('#alert-message').html(result);
}



function SuccessShoppingCartHandler(data) {
    if (data.success) {
        $.get('/Cart/Index/', function (partialView) {
            $('#shopping-cart').html(partialView);
        });
    }
};

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
        $('html,body').animate({
            scrollTop: 0
        }, 700);
    });
}
// ================================== EVENTS ======================================

function adminTabClickHandler(event, element) {
    var $this = $(this);
    $this.addClass('active');
    event.preventDefault();
    $.get(element.href, function (partialView) {
        $('#adminContent').html(partialView);
    });
};

function productTabClickHandler(event, element, elementId) {
    event.preventDefault();

    var $this = $(this);
    var viewProductContent = $('#view-product-content');

    $this.addClass('active');  
    $.get(element.href, { productId: elementId }, function (partialView) {
        viewProductContent.html(partialView);
    });
};

function loginClickHandler(event, element) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, function (data) {
        $('#dialogContent').html(data);
        $('#loginModal').modal('show');
    });
}

function getMethodClickHandler(event, element, elementToUpdate) {
    $.ajaxSetup({ cache: false });
    event.preventDefault();
    $.get(element.href, function (data) {
        $(elementToUpdate).html(data);
    });
}

$('.nav-tabs').on('click', 'li', function () {
    $('.nav-tabs li.active').removeClass('active');
    $(this).addClass('active');
});

$('.list-group-item').on('click', function (e) {
    $('.list-group').find('.active').removeClass('active');
    $(this).addClass('active');
});
