$(document).ready(function () {

    const form = $('#contactForm');

    function clearForm() {
        form[0].reset();
        $('input[name="Id"]').val('');
        form.validate().resetForm();
        form.find('.is-valid, .is-invalid')
            .removeClass('is-valid is-invalid');
    }
    
    $('#addContactBtn').on('click', function () {
        clearForm();
        $('#modalTitle').text('Create contact');
        form.attr('action', '/Contact/Create');
    });
    
    $(document).on('click', '.edit-btn', function () {
        clearForm();

        $('#modalTitle').text('Edit contact');
        form.attr('action', '/Contact/Edit');

        $('input[name="Id"]').val($(this).data('id'));
        $('#Name').val($(this).data('name'));
        $('#MobilePhone').val($(this).data('phone'));
        $('#JobTitle').val($(this).data('job'));
        $('#BirthDate').val($(this).data('birth'));
    });
    
    $('#contactModal').on('hidden.bs.modal', function () {
        clearForm();
        $('#addContactBtn').trigger('focus');
    });
});
