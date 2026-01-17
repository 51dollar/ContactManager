$(document).ready(function () {

    $('#contactsTable').DataTable({
        paging: true,
        searching: true,
        ordering: true,
        info: true,
        lengthChange: false,
        pageLength: 10,
        language: {
            search: "Search:",
            info: "Showing _START_ to _END_ of _TOTAL_ contacts",
            infoEmpty: "No contacts",
            zeroRecords: "Nothing found"
        },
        columnDefs: [
            { orderable: false, targets: 4 }
        ]
    });

});