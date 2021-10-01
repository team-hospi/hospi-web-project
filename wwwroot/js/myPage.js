function passwordCheck() {
    var p1 = document.getElementById('newPW1').value;
    var p2 = document.getElementById('newPW2').value;
    if (p1 != p2) {
        alert("비밀번호가 일치하지 않습니다.");
        return false;
    }
    else return true;
}

function editBirth() {
    document.getElementById('editBtn1').style.display = "none";
    document.getElementById('cancelBtn1').style.display = "";
    document.getElementById('editBirthDiv').style.display = "";

    var year = document.getElementById('birthyear');
    var month = document.getElementById('birthmonth');
    var day = document.getElementById('birthday');

    year.value = "";
    month.value = "";
    day.value = "";
}

function cancelBirth() {
    document.getElementById('editBtn1').style.display = "";
    document.getElementById('cancelBtn1').style.display = "none";
    document.getElementById('editBirthDiv').style.display = "none";
}

function saveBirth() {
    var year = document.getElementById('birthyear');
    var month = document.getElementById('birthmonth');
    var day = document.getElementById('birthday');
    var birth = year.value + "-" + month.value + "-" + day.value;

    if (checkValidDate(birth)) {
        document.getElementById('editBtn1').style.display = "";
        document.getElementById('cancelBtn1').style.display = "none";
        document.getElementById('editBirthDiv').style.display = "none";
        document.getElementById('birth').innerHTML = birth;
        return true;
    } else {
        alert('잘못된 날짜입니다. 다시 입력해주세요.');
        var year = document.getElementById('birthyear');
        var month = document.getElementById('birthmonth');
        var day = document.getElementById('birthday');

        year.value = "";
        month.value = "";
        day.value = "";
        return false;
    }
}

function editPhone() {
    document.getElementById('editBtn2').style.display = "none";
    document.getElementById('cancelBtn2').style.display = "";
    document.getElementById('editPhoneDiv').style.display = "";
    document.getElementById('phone_input').value = "";
}

function cancelPhone() {
    document.getElementById('editBtn2').style.display = "";
    document.getElementById('cancelBtn2').style.display = "none";
    document.getElementById('editPhoneDiv').style.display = "none";
}

function savePhone() {
    var phone = document.getElementById('phone_input').value;
    var regPhone = /^01([0|1|6|7|8|9])-?([0-9]{3,4})-?([0-9]{4})$/;

    if (regPhone.test(phone) === true) {
        var phoneHypen = phone.substring(0, 3) + "-" + phone.substring(3, 7) + "-" + phone.substring(7, phone.length);

        document.getElementById('phone_span').innerHTML = phoneHypen;
        document.getElementById('editBtn2').style.display = "";
        document.getElementById('cancelBtn2').style.display = "none";
        document.getElementById('editPhoneDiv').style.display = "none";

        return true;
    }
    else {
        alert('잘못된 전화번호입니다. 다시 입력해주세요.');
        document.getElementById('phone_input').value = "";
        return false;
    }
}

function checkValidDate(value) {
    var result = true;
    try {
        var date = value.split("-");
        var y = parseInt(date[0], 10),
            m = parseInt(date[1], 10),
            d = parseInt(date[2], 10);

        var dateRegex = /^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-.\/])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;
        result = dateRegex.test(d + '-' + m + '-' + y);
    } catch (err) {
        result = false;
    }
    return result;
}