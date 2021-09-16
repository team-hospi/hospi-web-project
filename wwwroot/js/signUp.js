function signUpBtn() {
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var password2 = document.getElementById("password2").value;
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

    var emailVal = $("#email").val();
    var regExp = /^[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*.[a-zA-Z]{2,3}$/i;

    if ($(privacyPolicyAgree).prop("checked") == false) {
        document.getElementById("checktext").value = "이용약관에 동의해주세요.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    if (emailVal.match(regExp) == null) {
        document.getElementById("checktext").value = "email 형식이 잘못되었습니다.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else if (password.length <= 7) {
        document.getElementById("checktext").value = "패스워드 형식이 잘못되었습니다.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else if (password != password2) {
        document.getElementById("checktext").value = "패스워드 재확인이 다릅니다."
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else if (name.length == 0) {
        document.getElementById("checktext").value = "이름 형식이 잘못되었습니다.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else if (sex != "남" && sex != "여") {
        document.getElementById("checktext").value = "성별을 선택해주세요.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else if (year.length == 0 || month.length == 0 || day.length == 0 || Number(month) > 12 || Number(month) < 1)
    {
        document.getElementById("checktext").value = "생년월일 형식이 잘못되었습니다.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else if (Number(month < 12) || Number(month > 0))
    {
        if (Number(month) == 2) {
            if (Number(year) % 4 === 0 && Number(year) % 100 !== 0 || Number(year) % 400 === 0) {
                if (Number(day) > 29 || Number(day) < 1) {
                    document.getElementById("checktext").value = "생년월일 형식이 잘못되었습니다.";
                    document.getElementById("checktext").style.display = "block";
                    return false;
                }
            }
            else {
                if (Number(day) > 28 || Number(day) < 1) {
                    document.getElementById("checktext").value = "생년월일 형식이 잘못되었습니다.";
                    document.getElementById("checktext").style.display = "block";
                    return false;
                }
            }
        }
        else if (Number(month) != 2 && Number(month) % 2 == 0) {
            if (Number(day) > 31 || Number(day) < 1) {
                document.getElementById("checktext").value = "생년월일 형식이 잘못되었습니다.";
                document.getElementById("checktext").style.display = "block";
                return false;
            }
        }
        else if (Number(month) != 2 && Number(month) % 2 == 1) {
            if (Number(day) > 31 || Number(day) < 1) {
                document.getElementById("checktext").value = "생년월일 형식이 잘못되었습니다.";
                document.getElementById("checktext").style.display = "block";
                return false;
            }
        }
    }
    else if (phone.length != 11)
    {
        document.getElementById("checktext").value = "핸드폰번호 형식이 잘못되었습니다.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else {
        return true;
    }
}

function reinput(){
    document.getElementById("checktext").value = "";
    document.getElementById("checktext").style.display = "none";
}

