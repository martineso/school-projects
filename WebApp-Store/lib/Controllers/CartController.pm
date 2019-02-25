use Dancer2;
use Data::Dumper;
require "$FindBin::Bin/../lib/Controllers/DbController.pm";

sub init_cart {
    if(! defined session("cart")) {
        my @cart = ();
        session "cart" => \@cart;
    }
}

sub get_cart {
    if(! defined session("cart")) {
        init_cart();
        return session("cart");
    } else {
        return session("cart");
    }
}

sub add_to_cart {
    my $product_id = shift;
    my $cart = get_cart();

    push @{$cart}, $product_id;
    session->delete("cart");
    session "cart" => $cart;
}

sub get_products_from_cart {
    my @cart = @{get_cart()};
    my %cart_items_hash;

    for my $item (@cart) {
        if($cart_items_hash{$item}) {
            $cart_items_hash{$item} = $cart_items_hash{$item} + 1;
        } else {
            $cart_items_hash{$item} = 1;
        }
    }

    my @item_ids = keys %cart_items_hash;

    my $params = join ', ' => ('?') x scalar @item_ids;
    my $sth = database->prepare("SELECT * FROM products 
                                JOIN products_desc 
                                ON products.id=products_desc.product_id
                                WHERE products.id IN ($params)");
    $sth->execute(@item_ids);

    my @result_set;
    while(my $row = $sth->fetchrow_hashref) {
        
        my $row_res = {
            'id' => $row->{product_id},
            'productName' => $row->{product_name},
            'price' => $row->{price},
            # This line will fetch the number of items that have been
            # added to the cart
            'quantity' => $cart_items_hash{$row->{product_id}},
            'imageUrl' => $row->{image_url},
            'description' => $row->{description},
            'onSale' => $row->{on_sale}
        };

        push @result_set, $row_res;
    }
    return \@result_set;  
}

sub get_products_from_cart_json {
    return to_json(get_products_from_cart());
}

sub empty_cart {
    session->delete("cart");
}

sub get_subtotal {
    my @products = @{get_products_from_cart()};
    my $subtotal = 0;

    for my $product (@products) {
        $subtotal = $subtotal + ($product->{price} * $product->{quantity});
    }
    return $subtotal;
}

sub get_cart_count {
    my $size = @{get_cart()};

    return $size;
}

true;