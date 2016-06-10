function LogOut() {
    window.location.replace('/Home/Index');
}

function Search() {
    location.replace('/Product/Index?search=' + $('#InputSearch').val());
}

function ChangeUser() {
    var username = $('#ChangeUsernameInput').val();
    var name = $('#ChangeNameInput').val();
    var email = $('#ChangeEmailInput').val();

    var oldpassword = $('#ChangePWInputOld').val();

    var newpassword = $('#ChangePWInputNew').val();
    var newpasswordcheck = $('#ChangePWInputNew').val();

    if (newpassword != newpasswordcheck) {
        alert("The new passwords don't match!");
    }

    $.ajax({
        type: "GET",
        url: "/User/ChangeUserInfo",
        data: { userName: username, name: name, email: email, password: newpassword, oldpassword: oldpassword },
        dataType: "json",
        success: function (data) {
            if (data == 1) {
                alert('Info changed succesfully!')
            }
            else {
                alert('The password is incorrect!');
            }
        },
        failure: function (data) {
        }
    });

}

function RemoveShipping(shippingId) {
    $.ajax({
        type: "GET",
        url: "/User/RemoveShippingAddres",
        data: { id: shippingId },
        dataType: "json",
        success: function (data) {
            location.reload();
        },
        failure: function (data) {
        }
    });
}

function AddShipping() {
    var Country = $('#CountryInput').val();
    var Name = $('#NameInput').val();
    var Address = $('#AddressInput').val();
    var City = $('#CityInput').val();
    var Postalcode = $('#PostalCodeInput').val();
    var Phonenumber = $('#PhoneNumberInput').val();

    if (Country == "") {
        alert('Please fill in a country!');
        return;
    }
    if (Name == "") {
        alert('Please fill in a name!');
        return;
    }
    if (Address == "") {
        alert('Please fill in an address!');
        return;
    }
    if (City == "") {
        alert('Please fill in a city!');
        return;
    }
    if (Postalcode == "") {
        alert('Please fill in a postalcode!');
        return;
    }

    $.ajax({
        type: "GET",
        url: "/User/AddShippingAddress",
        data: { address: Address, city: City, province: City, postalcode: Postalcode, phonenumber: Phonenumber },
        dataType: "json",
        success: function (data) {
            if (data == 1) {
                location.reload();
            }
        },
        failure: function (data) {
        }
    });
}

function AddComment() {
    var comment = $('#CommentInput').val();

    if (comment == "") {
        alert('Please fill in a comment');
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Product/AddComment",
        data: { commentText: comment },
        dataType: "json",
        success: function (data) {
        },
        failure: function (data) {
        }
    });
    location.reload();
}

function JoinDrop() {
    $.ajax({
        type: "POST",
        url: "/Product/JoinDrop",
        dataType: "json",
        success: function (data) {
            if (data == '1') {
                alert('Drop succesfully joined!')
            }
            else {
                alert('You can only join a drop once!')
            }
        },
        failure: function (data) {
        }
    });
}