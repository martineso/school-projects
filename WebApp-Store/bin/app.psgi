#!/usr/bin/env perl

use strict;
use warnings;
use FindBin;
use lib "$FindBin::Bin/../lib";


# use this block if you don't need middleware, and only have a single target Dancer app to run here
use WebApp::Store;

WebApp::Store->to_app;

use Plack::Builder;

builder {
    enable 'Deflater';
    WebApp::Store->to_app;
}



=begin comment
# use this block if you want to include middleware such as Plack::Middleware::Deflater

use WebApp::Store;
use Plack::Builder;

builder {
    enable 'Deflater';
    WebApp::Store->to_app;
}

=end comment

=cut

=begin comment
# use this block if you want to include middleware such as Plack::Middleware::Deflater

use WebApp::Store;
use WebApp::Store_admin;

builder {
    mount '/'      => WebApp::Store->to_app;
    mount '/admin'      => WebApp::Store_admin->to_app;
}

=end comment

=cut

