function Submit(valid) {
    if (Page_ClientValidate(valid)) {
        swal.fire({
            icon: 'question',
            title: 'Do you want to Submit ?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                showloading('1');
                $("#Body_btn_Submit").click();
            }
        });
    }
}
function Draft(valid) {
    var name = $('#Body_Vendor_Name').val();
    var pic = $('#Body_Vendor_PIC').val();
    /*var pic = $("#Body_Vendor_PIC").val;*/
    var GM = $("#Body_GM_Email").val();
    var Mail = $("#Body_Email").val();
    if (Page_ClientValidate(valid)) {
        if (name == "" && pic == "" && GM == "" && Mail == "") {
            swal.fire({
                icon: 'warning',
                title: 'Please fill data.',
                text: '',
                timer: 5000
            })
        }
        else {
            swal.fire({
                icon: 'question',
                title: 'Do you want to Save Draft ?',
                text: '',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No'
            }).
                then(function (result) {
                    if (result.value) {
                        showloading('1');
                        $("#Body_btn_Draft").click();
                    }
                });
        }
    }
}