window.addEventListener("load", function () {
    var MOVEING_PX = 2,
        AUTO_TIME = 2000,
        slide = document.getElementById("slide"),
        indi = document.createElement("ul"),
        slideCnt = slide.getElementsByClassName("cnt"),
        slideCntItem = slideCnt[0].getElementsByTagName("li"),
        prevBtn = slide.getElementsByClassName("prev"),
        nextBtn = slide.getElementsByClassName("next"),
        playBtn = slide.getElementsByClassName("play"),
        stopBtn = slide.getElementsByClassName("stop"),
        playSet = null,
        before = 0,
        after = 0,
        moveIng = false;

    // init
    slideCntItem[0].style.left = 0;
    playBtn[0].style.display = "block";
    var indi = document.createElement("ul");
    for (var i = 0; i < slideCntItem.length; i++) {
        indi.innerHTML += "<li></li>";
    }
    indi.classList.add("indi");
    indi.children[0].classList.add("on");
    slide.append(indi);

    for (var j = 0; j < indi.children.length; j++) {
        indiClick(j);
    }

    // initEvnet
    nextBtn[0].addEventListener("click", function (e) {
        if (!moveIng) {
            after++;
            if (after >= slideCntItem.length) {
                after = 0;
            }
            move(after, before, "next");
            before = after;
        }
    });

    prevBtn[0].addEventListener("click", function (e) {
        if (!moveIng) {
            after--;
            if (after < 0) {
                after = slideCntItem.length - 1;
            }
            move(after, before);
            before = after;
        }
    });

    playBtn[0].addEventListener("click", function () {
        playBtn[0].style.display = "none";
        stopBtn[0].style.display = "block";
        playSet = setInterval(function () {
            if (!moveIng) {
                after++;
                if (after >= slideCntItem.length) {
                    after = 0;
                }
                move(after, before, "next");
                before = after;
            }
        }, AUTO_TIME);
    });

    stopBtn[0].addEventListener("click", function () {
        playBtn[0].style.display = "block";
        stopBtn[0].style.display = "none";
        clearInterval(playSet);
    });

    function indiClick(target) {
        indi.children[target].addEventListener("click", function () {
            if (!moveIng) {
                after = target;
                if (after > before) {
                    move(after, before, "next");
                } else if (after < before) {
                    move(after, before);
                }
                before = after;
            }
        });
    }

    function move(after, before, type) {
        var nextX = type === "next" ? slide.offsetWidth : slide.offsetWidth * -1,
            prevX = 0,
            set = null;
        set = setInterval(function () {
            moveIng = true;
            if (type === "next") {
                nextX -= MOVEING_PX;
                slideCntItem[after].style.left = nextX + "px";
                if (nextX <= 0) {
                    clearInterval(set);
                    nextX = slide.offsetWidth;
                    moveIng = false;
                }
                prevX -= MOVEING_PX;
            } else {
                nextX += MOVEING_PX;
                slideCntItem[after].style.left = nextX + "px";
                if (nextX >= 0) {
                    clearInterval(set);
                    nextX = slide.offsetWidth * -1;
                    moveIng = false;
                }
                prevX += MOVEING_PX;
            }
            slideCntItem[before].style.left = prevX + "px";
        });
        indi.children[before].classList.remove("on");
        indi.children[after].classList.add("on");
    }
});