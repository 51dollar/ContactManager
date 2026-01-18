$(() => {
    const tableSelector = '#contactsTable';

    DataTable.type('num', 'className', 'dt-body-center');
    
    $(tableSelector).DataTable({
        paging: true,
        searching: true,
        ordering: true,
        info: true,
        lengthChange: true,
        pageLength: 10,

        language: {
            search: '',
            searchPlaceholder: 'Search contacts...',
            info: 'Showing _START_ to _END_ of _TOTAL_ contacts',
            infoEmpty: 'No contacts',
            zeroRecords: 'Nothing found'
        },

        columnDefs: [
            {
                targets: '_all',
                className: 'text-center'
            },
            {
                targets: 4,
                orderable: false
            }
        ]
    });
});