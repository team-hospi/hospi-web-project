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

// 진료과 등록, 삭제 기능
function appendDepartment() {
    var deleteBtn = document.createElement("input");
    deleteBtn.type = "button";
    deleteBtn.value = "삭제";
    deleteBtn.className = "btn btn-dark";
    deleteBtn.onclick = "deleteDepartment()";

    var table = document.getElementById('departmentList');
    var newRow = table.insertRow();
    var departmentCell = newRow.insertCell(0);
    var btnCell = newRow.insertCell(1);

    departmentCell.className = "text-center";
    btnCell.className = "text-center";

    var departmentInput = document.getElementById("departmentName");

    departmentCell.innerHTML = "<input type=\"text\" class=\"form-control\" id=\"departmentName\" value=\"" + departmentInput.value + "\" readonly />";
    btnCell.innerHTML = "<input type=\"button\" class=\"btn btn-dark\" value=\"삭제\" onclick=\"deleteDepartment(this)\" />";

    departmentInput.value = "";
}

function deleteDepartment(btn) {
    var index = btn.parentNode.parentNode.rowIndex;
    var table = document.getElementById('departmentList');
    table.deleteRow(index);
}