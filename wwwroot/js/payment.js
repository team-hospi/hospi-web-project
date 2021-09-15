function PayBtn()
{
    var month = document.getElementById("month").value;
    var year = document.getElementById("year").value;

    var birthyear = document.getElementById("birthyear").value;
    var birthmonth = document.getElementById("birthmonth").value;
    var birthday = document.getElementById("birthday").value;

    if ($(paymentAgree).prop("checked") == false) {
        document.getElementById("checktext").value = "결제정보제공에 동의해주세요.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    if (document.getElementById("card1").value.length < 4 || document.getElementById("card2").value.length < 4 || document.getElementById("card3").value.length < 4 || document.getElementById("card4").value.length < 4) {
        document.getElementById("checktext").value = "카드 번호가 잘못되었습니다.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else if (month.length < 2 || year.length < 4 || Number(month) > 12 || Number(month) < 1) {
        document.getElementById("checktext").value = "카드 유효일자가 잘못되었습니다.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else if (document.getElementById("name").value.length == 0) {
        document.getElementById("checktext").value = "이름을 입력해주세요";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else if (birthyear.length < 4 || birthmonth.length < 2 || birthday.length < 2 || Number(birthmonth) > 12 || Number(birthmonth) < 1) {
        document.getElementById("checktext").value = "생년월일 형식이 잘못되었습니다.";
        document.getElementById("checktext").style.display = "block";
        return false;
    }
    else {
        return true;
    }
}