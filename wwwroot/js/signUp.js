function signUpBtn() {
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var sex_length = document.getElementsByName("sex").length;
    var sex = "";
    for (var i = 0; i < sex_length; i++) {
        if (document.getElementsByName("sex")[i].checked == true) {
            sex = document.getElementsByName("sex")[i].value;
        }
    }
    var year = document.getElementById("year").value;
    var month = document.getElementById("month").value;
    var day = document.getElementById("day").value;
    var birth = year + "-" + month + "-" + day;
    var phone = document.getElementById("phone").value;
    var name = document.getElementById("name").value;

    document.getElementById("_email").value = email;
    document.getElementById("_password").value = password;
    document.getElementById("_sex").value = sex;
    document.getElementById("_birth").value = birth;
    document.getElementById("_phone").value = phone;
    document.getElementById("_name").value = name;

    return true;
}