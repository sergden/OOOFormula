// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('body').on('click', '.password-control', function () {
    if ($('#password-input').attr('type') == 'password') {
        $(this).addClass('view');
        $('#password-input').attr('type', 'text');
    }
    else {
        $(this).removeClass('view');
        $('#password-input').attr('type', 'password');
    }
    return false;
});

$('body').on('click', '.password-control-new', function () {
    if ($('#password-input-new').attr('type') == 'password') {
        $(this).addClass('view');
        $('#password-input-new').attr('type', 'text');
    }
    else {
        $(this).removeClass('view');
        $('#password-input-new').attr('type', 'password');
    }
    return false;
});

$('body').on('click', '.password-control-confirm', function () {
    if ($('#password-input-confirm').attr('type') == 'password') {
        $(this).addClass('view');
        $('#password-input-confirm').attr('type', 'text');
    }
    else {
        $(this).removeClass('view');
        $('#password-input-confirm').attr('type', 'password');
    }
    return false;
});