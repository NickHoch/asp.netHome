$(function () {
    let basis = parseFloat($('#basis').find(':selected').val());
    let size = parseInt($('#size').find(':selected').val());
    let sauce = parseInt($('#sauce').find(':selected').val());
    let price = Math.round(basis * size + sauce);
    let weight = size * 10;

    $('#price').html(price + ' uah');
    $('#weight').html(weight);

    $('#availableIngrid > div').click(function () {
        let ingrid = $(this);
        if(ingrid.is('#availableIngrid > div')) {
            $(ingrid).appendTo('#selectedIngrid');
            price += parseFloat($(this).data('price') / 100);
            weight += parseInt($(this).data('weight'));
        } else {
            $(ingrid).appendTo('#availableIngrid');
            price -= parseFloat($(this).data('price') / 100);
            weight -= parseInt($(this).data('weight'));
        }
        $('#price').html(Math.round(price * 10) / 10 + ' uah');
        $('#weight').html(weight);
    });

    // $('#selectedIngrid > div').click(function () {
    //     $($(this)).appendTo('#availableIngrid');
    //     // price += parseFloat($(this).dataset.price) / 100;
    //     // $('#price').html(price + ' uah');
    // });

    function CountSum() {
        price = basis * size + sauce;
        $('#selectedIngrid > div').each(function() {
            price += parseFloat($(this).data('price') / 100);
        });
        $('#price').html(Math.round(price * 10) / 10 + ' uah');
    }

    $('#basis').change( function() {
        basis = parseFloat($('#basis').find(':selected').val());
        CountSum();
    });
    $('#size').change( function() {
        size = parseInt($('#size').find(':selected').val());
        CountSum();
        weight = size * 10;
        $('#selectedIngrid > div').each(function() {
            weight += parseFloat($(this).data('weight'));
        });
        $('#weight').html(weight);
    })
    $('#sauce').change( function() {
        sauce = parseInt($('#sauce').find(':selected').val());
        CountSum();
    })

    $('#makeOrder').click(function() {
        let size = $('#size').find(':selected').text();
        let sauce = $('#sauce').find(':selected').text();
        let basis = $('#basis').find(':selected').text();
        let ingrid = '';
        $('#selectedIngrid > div').each(function() {
            ingrid += '\t' + $(this).data('name') + ' - ';
            ingrid += parseFloat($(this).data('weight')) + 'g - ';
            ingrid += parseFloat($(this).data('price')) / 100 + 'uah' + '\n';
        });
        confirm('Cheque: \n' + 'Size - ' + size + ';\n' + 'Sauce - ' + sauce + ';\n' + 'Basis - ' + basis + ';\n' + 'Ingridients: \n' + ingrid + 'Total weight - ' + weight + 'g;\n' + 'Price - ' + price + 'uah.');
    });
});