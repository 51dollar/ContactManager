$(() => {
    const tableSelector = '#contactsTable';
    const selected = new Set();

    const $editBtn = $('#toolbarEditBtn');
    const $deleteBtn = $('#toolbarDeleteBtn');
    const $bulkForm = $('#bulkActionsForm');
    const $bulkIds = $('#bulkIdsContainer');
    const $selectAll = $('#selectAll');

    const renderToolbar = () => {
        const count = selected.size;
        $editBtn.toggleClass('d-none', count !== 1);
        $deleteBtn.toggleClass('d-none', count === 0);
    };

    const syncBulkForm = () => {
        $bulkIds.empty();
        selected.forEach(id => {
            $bulkIds.append(
                `<input type="hidden" name="ids" value="${id}" />`
            );
        });
    };

    const restoreSelection = () => {
        $('.row-check').each(function () {
            const id = this.value;
            const checked = selected.has(id);

            this.checked = checked;
            $(this)
                .closest('tr')
                .toggleClass('table-active', checked);
        });

        renderToolbar();
        syncBulkForm();
    };

    const table = $(tableSelector).DataTable({
        paging: true,
        searching: true,
        ordering: true,
        info: true,
        lengthChange: true,
        pageLength: 10,

        order: [],
        
        language: {
            search: '',
            searchPlaceholder: 'Search contacts...',
            info: 'Showing _START_ to _END_ of _TOTAL_ contacts',
            infoEmpty: 'No contacts',
            zeroRecords: 'Nothing found'
        },

        columnDefs: [
            {targets: 0, orderable: false, searchable: false},
            {targets: '_all', className: 'text-center'}
        ],

        drawCallback: restoreSelection
    });

    $(document).on('change', '.row-check', function () {
        const id = this.value;

        this.checked
            ? selected.add(id)
            : selected.delete(id);

        $(this)
            .closest('tr')
            .toggleClass('table-active', this.checked);

        renderToolbar();
        syncBulkForm();
    });

    $selectAll.on('change', function () {
        const checked = this.checked;

        $('.row-check').each(function () {
            this.checked = checked;

            const id = this.value;
            checked
                ? selected.add(id)
                : selected.delete(id);

            $(this)
                .closest('tr')
                .toggleClass('table-active', checked);
        });

        renderToolbar();
        syncBulkForm();
    });

    $editBtn.on('click', () => {
        const id = [...selected][0];
        const $row = $(`tr[data-id="${id}"]`);

        $('#modalTitle').text('Edit contact');
        $('#contactForm').attr('action', '/Contact/Edit');

        $('#Id').val($row.data('id'));
        $('#Name').val($row.data('name'));
        $('#MobilePhone').val($row.data('phone'));
        $('#JobTitle').val($row.data('job'));
        $('#BirthDate').val($row.data('birth'));

        const modal = bootstrap.Modal.getOrCreateInstance(
            document.getElementById('contactModal')
        );
        modal.show();
    });

    $deleteBtn.on('click', () => {
        const count = selected.size;

        if (!confirm(`Delete ${count} contact${count > 1 ? 's' : ''}?`))
            return;

        $bulkForm.submit();
    });
});