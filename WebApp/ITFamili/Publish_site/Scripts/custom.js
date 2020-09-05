function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}


function deleteCookie(name) {
    document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

function RegisterUser() {

    var fullName = $("#txtFullName").val();

    var cellNumber = $("#txtCellNumber").val();

    if (fullName !== "" && cellNumber !== "") {
        $.ajax(
            {
                url: "/Account/RegisterUser",
                data: { fullName: fullName, cellNumber: cellNumber },
                type: "POST"

            }).done(function (result) {
            if (result.includes("true")) {
                $("#activate-form").css('display', 'block');
                $("#register-form").css('display', 'none');
                setCookie("itfamili", result.split('|')[1], 100);
            }
            else if (result === "false") {
                $("#error-register").html('خطایی رخ داده است. لطفا مجدادا تلاش کنید.');
                $("#error-register").css('display', 'block');

            }
        });
    }
    else {
        $("#error-register").html('تمامی فیلد های بالا را تکمیل نمایید.');
        $("#error-register").css('display', 'block');
    }
}

function ActiveUser() {

    var code = getCookie("itfamili");
    var activationCode = $("#txtActivationCode").val();

    if (activationCode !== "") {
        $.ajax(
            {
                url: "/Account/Activate",
                data: { activationCode: activationCode, code: code },
                type: "POST"

            }).done(function (result) {
            if (result.includes("true")) {
                $("#error-activate").css('display', 'none');
                $("#success-activate").css('display', 'block');
            }
            else if (result === "false") {
                $("#error-activate").html('کد وارد شده صحیح نمی باشد.');
                $("#error-activate").css('display', 'block');
                $("#success-activate").css('display', 'none');
                }
        });
    }
    else {
        $("#error-activate").html('کد فعالسازی را وارد نمایید.');
        $("#error-activate").css('display', 'block');
    }
}

