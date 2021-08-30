// 개원, 휴원 선택에 따라 시간 선택을 활성화, 비활성화하는 코드
function saturdayTimeSelectActive() {
    document.getElementById('saturdayOpenHour').disabled = false;
    document.getElementById('saturdayOpenMin').disabled = false;
    document.getElementById('saturdayCloseHour').disabled = false;
    document.getElementById('saturdayCloseMin').disabled = false;
}

function saturdayTimeSelectDisabled() {
    document.getElementById('saturdayOpenHour').disabled = true;
    document.getElementById('saturdayOpenMin').disabled = true;
    document.getElementById('saturdayCloseHour').disabled = true;
    document.getElementById('saturdayCloseMin').disabled = true;
}

function holidayTimeSelectActive() {
    document.getElementById('holidayOpenHour').disabled = false;
    document.getElementById('holidayOpenMin').disabled = false;
    document.getElementById('holidayCloseHour').disabled = false;
    document.getElementById('holidayCloseMin').disabled = false;
}

function holidayTimeSelectDisabled() {
    document.getElementById('holidayOpenHour').disabled = true;
    document.getElementById('holidayOpenMin').disabled = true;
    document.getElementById('holidayCloseHour').disabled = true;
    document.getElementById('holidayCloseMin').disabled = true;
}

// 다음 주소 api
function daumPostcode() {
    new daum.Postcode({
        oncomplete: function (data) {
            // 팝업에서 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.

            // 각 주소의 노출 규칙에 따라 주소를 조합한다.
            // 내려오는 변수가 값이 없는 경우엔 공백('')값을 가지므로, 이를 참고하여 분기 한다.
            var addr = ''; // 주소 변수
            var extraAddr = ''; // 참고항목 변수

            //사용자가 선택한 주소 타입에 따라 해당 주소 값을 가져온다.
            if (data.userSelectedType === 'R') { // 사용자가 도로명 주소를 선택했을 경우
                addr = data.roadAddress;
            } else { // 사용자가 지번 주소를 선택했을 경우(J)
                addr = data.jibunAddress;
            }

            // 사용자가 선택한 주소가 도로명 타입일때 참고항목을 조합한다.
            if (data.userSelectedType === 'R') {
                // 법정동명이 있을 경우 추가한다. (법정리는 제외)
                // 법정동의 경우 마지막 문자가 "동/로/가"로 끝난다.
                if (data.bname !== '' && /[동|로|가]$/g.test(data.bname)) {
                    extraAddr += data.bname;
                }
                // 건물명이 있고, 공동주택일 경우 추가한다.
                if (data.buildingName !== '' && data.apartment === 'Y') {
                    extraAddr += (extraAddr !== '' ? ', ' + data.buildingName : data.buildingName);
                }
                // 표시할 참고항목이 있을 경우, 괄호까지 추가한 최종 문자열을 만든다.
                if (extraAddr !== '') {
                    extraAddr = ' (' + extraAddr + ')';
                }
                // 조합된 참고항목을 해당 필드에 넣는다.
                // document.getElementById("extraAddress").value = extraAddr;

            } else {
                // document.getElementById("extraAddress").value = '';
            }

            // 우편번호와 주소 정보를 해당 필드에 넣는다.
            document.getElementById('postcode').value = data.zonecode;
            document.getElementById("address").value = addr + extraAddr;
            // 커서를 상세주소 필드로 이동한다.
            document.getElementById("detailAddress").focus();
        }
    }).open();
}

// 진료과 등록, 삭제 기능
function appendDepartment() {
    var deleteBtn = document.createElement("input");
    deleteBtn.type = "button";
    deleteBtn.value = "삭제";
    deleteBtn.className = "btn btn-dark";
    deleteBtn.onclick = "deleteDepartment()";

    var table = document.getElementById('departmentList');
    var newRow = table.insertRow();
    var newCell1 = newRow.insertCell(0);
    var newCell2 = newRow.insertCell(1);

    var departmentName = document.getElementById("departmentName").value;

    newCell1.innerText = departmentName;
    newCell2.innerHTML = "<input type=\"button\" class=\"btn btn-dark\" value=\"삭제\" onclick=\"deleteDepartment(this)\" />";
}

function deleteDepartment(btn) {
    var index = btn.parentNode.parentNode.rowIndex;
    var table = document.getElementById('departmentList');
    table.deleteRow(index);
}