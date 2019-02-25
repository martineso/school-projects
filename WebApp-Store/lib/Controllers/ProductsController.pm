use Dancer2;
use Dancer2::Serializer::JSON;
use DateTime;
use FindBin;
require "$FindBin::Bin/../lib/Controllers/DbController.pm";
use Data::Dumper;


sub get_emp_products_json {
    my $sth = database->prepare("SELECT products.*, 
                                        u1.username as added_by_user, 
                                        u2.username as last_edited_by_user 
                                        FROM products 
                                        JOIN users u1 ON products.added_by=u1.id 
                                        JOIN users u2 ON products.last_edited_by=u2.id;");
    $sth->execute();

    my @result_set;
    while(my $row = $sth->fetchrow_hashref) {
        
        my $row_res = {
            'id' => $row->{id},
            'productName' => $row->{product_name},
            'price' => $row->{price},
            'quantity' => $row->{quantity},
            'addedBy' => $row->{added_by_user},
            'addedOn' => $row->{added_on},
            'lastEdited' => $row->{last_edited},
            'lastEditedBy' => $row->{last_edited_by_user}
        };

        push @result_set, $row_res;
    }
    return to_json(\@result_set);
};

sub get_public_products_json {

    my $sth = database->prepare("SELECT * FROM products 
                            JOIN products_desc 
                            ON products.id=products_desc.product_id");

    $sth->execute();    

    my @result_set;
    while(my $row = $sth->fetchrow_hashref) {
        
        my $row_res = {
            'id' => $row->{product_id},
            'productName' => $row->{product_name},
            'price' => $row->{price},
            'quantity' => $row->{quantity},
            'imageUrl' => $row->{image_url},
            'description' => $row->{description},
            'onSale' => $row->{on_sale}
        };

        push @result_set, $row_res;
    }
    return to_json(\@result_set);                                    
}

sub add_product {
    my $product = shift;

    my $sth = database->prepare("INSERT INTO products(
                                                product_name, 
                                                price,
                                                quantity,
                                                added_by,
                                                added_on,
                                                last_edited,
                                                last_edited_by)
                                                VALUES (?,?,?,?,?,?,?)");

    $sth->execute(
        $product->{productName},
        $product->{price},
        $product->{quantity},
        $product->{addedBy},
        get_current_date(),
        get_current_date(),
        $product->{lastEditedBy}
    );

    $sth = database->prepare("INSERT INTO products_desc(
                                product_id)
                                SELECT id 
                                FROM products WHERE products.product_name = ?");
    $sth->execute($product->{productName});
}

sub edit_product {
    my $product = shift;
    my $sth = database->prepare("UPDATE products SET
                                                product_name = ?, 
                                                price = ?,
                                                quantity = ?,
                                                last_edited = ?,
                                                last_edited_by = ?
                                                WHERE id = ?");

    $sth->execute(
        $product->{productName},
        $product->{price},
        $product->{quantity},
        get_current_date(),
        $product->{lastEditedBy},
        $product->{id}
    );
}

sub product_exists {
    my $product_name = shift;
    my $res = database->quick_select('products', { product_name => $product_name});

    if($res) {
        return true;
    }
    return false;
}

sub get_current_date {
    my $dt = DateTime->now;
    my $dmy = $dt->dmy;
    my $hms = $dt->hms;

    return join(" ", $dmy, $hms);
}

true;