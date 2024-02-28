export default function Navbar(){

    return(
        <div>
            <nav class="navbar navbar-expand-lg navbar-light custom-bg-color2">
                <div class="container">
                    <a class="navbar-brand" href="google.com">Maverick Bank</a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarNav">
                        <ul class="navbar-nav ml-auto">
                            <li class="nav-item active">
                            <a class="nav-link" href="http://localhost:3000/Home">Home</a>
                            </li>
                            <li class="nav-item active">
                            <a class="nav-link" href="http://localhost:3000/Dashboard">Dashboard</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </div>
    )
}