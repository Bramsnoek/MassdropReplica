﻿function closePasswordModal() {
    $('#PasswordModal').modal('hide');
}

function LogIn() {
    var username = $('#LogInEmail').val();
    var password = $('#LogInPassword').val();

    if (username == "") {
        alert('Please fill in an username!');
        return;
    }
    if (password == "") {
        alert('Please fill in a password!');
        return;
    }

    $.ajax({
        type: "GET",
        url: "/Home/LogIn",
        data: { userName: username, password: password },
        dataType: "json",
        success: function (data) {
            if (data == 1) {
                window.location.replace("/Product/Index");
            }
            else {
                alert('The username or password is incorrect!');
            }
        },
        failure: function (data) {
        }
    });
}

function SignUp() {
    var username = $('#signUpEmail').val();
    var password = $('#signUpPassword').val();

    if (username == "") {
        alert('Please fill in an username!');
        return;
    }
    if (password == "") {
        alert('Please fill in a password!');
        return;
    }

    if (password.toLowerCase() == password) {
        alert('Your password must contain a capital letter!');
        return;
    }
    if (password.length <= 6) {
        alert('Your password must be longer than 6 characters!');
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Home/CreateUser",
        data: { userName: username, password: password },
        success: function (data) {
            $('#MailModal').modal('hide');
        },
        failure: function () {
            alert('failure');
        }
    });
}

//// This is called with the results from from FB.getLoginStatus().
function statusChangeCallback(response) {
    console.log('statusChangeCallback');
    console.log(response);
    // The response object is returned with a status field that lets the
    // app know the current login status of the person.
    // Full docs on the response object can be found in the documentation
    // for FB.getLoginStatus().
    if (response.status === 'connected') {
        // Logged into your app and Facebook.
        FacebookLogin();
    }
}

// This function is called when someone finishes with the Login
// Button.  See the onlogin handler attached to it in the sample
// code below.
function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '267702026915521',
        cookie: true,  // enable cookies to allow the server to access
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.5' // use graph api version 2.5
    });

    // Now that we've initialized the JavaScript SDK, we call
    // FB.getLoginStatus().  This function gets the state of the
    // person visiting this page and can return one of three states to
    // the callback you provide.  They can be:
    //
    // 1. Logged into your app ('connected')
    // 2. Logged into Facebook, but not your app ('not_authorized')
    // 3. Not logged into Facebook and can't tell if they are logged into
    //    your app or not.
    //
    // These three cases are handled in the callback function.

    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });

};

// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

// Here we run a very simple test of the Graph API after login is
// successful.  See statusChangeCallback() for when this call is made.
function FacebookLogin() {
    console.log('Welcome!  Fetching your information.... ');
    FB.api('/me', { fields: 'name,email,picture' }, function (response) {
        $.ajax({
            type: "POST",
            url: "/Home/FacebookLogIn",
            data: { name: response.name, email: response.email, imageurl: response.picture.data.url },
            dataType: "json",
            success: function (data) {
                if (data == '1') {
                    window.location.replace("/Product/Index");
                }
            },
            failure: function (data) {
            }
        });
    });
}