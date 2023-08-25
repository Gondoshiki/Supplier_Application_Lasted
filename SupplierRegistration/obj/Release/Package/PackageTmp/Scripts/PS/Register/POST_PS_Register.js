function Register(valid) {
    console.log(valid)
    if (Page_ClientValidate(valid)) {
        swal.fire({
            icon: 'question',
            title: 'Do you want to Registation ?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            console.log(result)
            if (result.value) {
                $("#btn_Register").click();
            }
        });
    }
}
