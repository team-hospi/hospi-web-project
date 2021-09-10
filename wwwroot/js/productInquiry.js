function inquiryBtn() {
    document.getElementById("kind").value = getKind();

    // TODO: 빈칸 예외처리 해야함

    return true;
}

function getKind() {
    var kind_length = document.getElementsByName("kind").length;
    var kind = "";
    for (var i = 0; i < kind_length; i++) {
        if (document.getElementsByName("kind")[i].checked == true) {
            kind = document.getElementsByName("kind")[i].value;
        }
    }

    return kind;
}