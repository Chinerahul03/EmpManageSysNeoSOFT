
function confirmDelete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            // Redirect to the delete action method
            window.location.href = '/EmployeeMasters/Delete/' + id;
        }
    })
}
$(document).ready(
    function () {
        var myData = document.getElementById('MsgData').value;
        if (myData != '') {
            Swal.fire({
                title: 'Success!',
                text: myData,
                icon: 'success',
                confirmButtonText: 'OK'
            });
        }
    }
 );
