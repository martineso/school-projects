package WebApp::Store;
use Dancer2;
use FindBin;
require "$FindBin::Bin/../lib/Controllers/UserController.pm";
require "$FindBin::Bin/../lib/Controllers/ProductsController.pm";
require "$FindBin::Bin/../lib/Controllers/CartController.pm";
require "$FindBin::Bin/../lib/Controllers/PaymentController.pm";
use Data::Dumper;

our $VERSION = '0.1';


hook 'before_template_render' => sub {
    my $tokens = shift;
    $tokens->{'landing_page_img_url'} = request->base . "/images/brand-pic.png";
    $tokens->{'products_url'} = '/products';
    $tokens->{'modal_css'} = request->base . 'css/modal.css';

    init_cart();
    session 'cart_items_count' => get_cart_count();
};

# ---
# ROUTES
# ---

get '/' => sub {
    template 'index' => { 
        'title' => 'Store' 
    };
};

any ['get' , 'post'] => '/login' => sub {
    my $err;

    if(request->method() eq "POST") {
        my $user = body_parameters->get('username');
        my $pass = body_parameters->get('password');
        
        my $curr_user = fetch_user($user);
        
        # if the user exists
        if($curr_user and validate_password($curr_user->{password}, $pass)) {
            session "logged_in" => true;
            session "username" => $curr_user->{username};
            session "user_id" => $curr_user->{id};

            # Check if the user is an employee and redirect accordingly
            if(is_employee($curr_user->{id})) {
                # Add to the session that the user is an employee
                session "is_employee" => true;

                redirect '/emp/' . $curr_user->{id} . '/products'
            } else {
                redirect '/products';
            }
        } else {
            $err = "The username or the password are incorrect!";
        }
    }

    template 'login' => {
        'title' => 'Login',
        'heading' => 'Login',
        'form_action' => uri_for('/login'),
        'register_url' => uri_for('/register'),
        'error_message' => $err
    };

};

any ['get', 'post'] => '/register' => sub {

    my $err;

    if(request->method() eq "POST") {
        my $user = body_parameters->get('username');
        my $email = body_parameters->get('email_address');
        my $pass = body_parameters->get('password');

        # check if user exists
        my $fetched_user = fetch_user($user);
        if($fetched_user) {
            $err = "Username already exists!";
        } else {
            # Insert into the db
            insert_user_into_db({
                'username' => $user,
                'email_address' => $email,
                'password' => $pass
            });

            redirect '/';
        }

    }
    
    template 'register' => {
        'title' => 'Register',
        'heading' => 'Register',
        'form_action' => uri_for('/register'),
        'validation_script' => (request->base . 'js/validation_script.js'),
        'error_message' => $err
    };
};

get '/logout' => sub {
   app->destroy_session;
   redirect '/';
};

# ADMIN ROUTES

get '/emp/:id/products' => sub {

    if(!session("logged_in") and !session("is_employee")) {
        forward '/error', {
            error_message => "You are not authorized to view this page! \n"
        };
    }

    template 'emp_products' => {
        'title' => 'Products',
        'form_heading' => 'Add new item',
        'xeditable_ng_module' => request->base . 'js/lib/xeditable.min.js',
        'store_app_angular' => request->base . 'js/store-app.js',
        'products_controller' => request->base . 'js/productsController.js'
    };
};

# Routes for adding and updating a product

post '/products' => sub {
    if(!session("logged_in") and !session("is_employee")) {
        forward '/error', { 
            error_message => "Access denied! \n"
        };
    }

    my $new_product = from_json(request->body);
    $new_product->{addedBy} = session("user_id");
    $new_product->{lastEditedBy} = session("user_id");

    if(product_exists($new_product->{productName})) {
        status 418;
        return "The product already exists.";
    } else {
        # we are good to go, add the product to the database
        add_product($new_product);
    }
};

patch '/products/:id' => sub {

    # only allow logged employess to edit a product
    if(!session("logged_in") and !session("is_employee")) {
        forward '/error', { 
            error_message => "Access denied! \n"
        };
    }

    my $new_product = from_json(request->body);
    edit_product($new_product);
};

# USER ROUTES

get '/products' => sub {

    template 'products' => {
        'title' => 'Products',
        'modal_css' => request->base . 'css/modal.css',
        'xeditable_ng_module' => request->base . 'js/lib/xeditable.min.js',
        'store_app_angular' => request->base . 'js/store-app.js',
        'user_products_controller' => request->base . 'js/userProductsController.js'
    };

};

get '/products/all' => sub {

    if(session("is_employee")) {
        return get_emp_products_json();
    } else {
        return get_public_products_json();
    }
};

# CART AND CHECKOUT

get '/cart' => sub {

    template 'cart/cart' => {
        'title' => 'Your cart',
        'xeditable_ng_module' => request->base . 'js/lib/xeditable.min.js',
        'store_app_angular' => request->base . 'js/store-app.js',
        'cart_controller' => request->base . 'js/cartController.js',
        'checkout_url' => uri_for('/cart/checkout'),
    };
};

get '/cart/all' => sub {
    return get_products_from_cart_json();
};

post '/cart/:id' => sub {
    add_to_cart(route_parameters->get('id'));
    status 200;
    return "Product has been added successfully";
};

get '/cart/checkout' => sub {
    template 'cart/checkout' => {
        'title' => 'Checkout',
        'xeditable_ng_module' => request->base . 'js/lib/xeditable.min.js',
        'store_app_angular' => request->base . 'js/store-app.js',
        'payment_controller' => request->base . 'js/paymentController.js',
        'payment_url' => get_noreg_payment_url("STORE_APP_PAYMENT", "TESTING")
    };
};

get '/cart/clear' => sub {
    empty_cart();
    status 200;
    return "Cart has been cleared!";
};

get 'error' => sub {
    template 'error' => {
        'error_message' => param('error_message')
    };
};

true;
