function getChallangeItem(isCreate, challange) {
    let str = "<div class='swiper-slide'>" +
        "<a href='#'>";

    if (isCreate) str += "<div class='challenge challenge_create'>";
    else str += "<div class='challenge challenge_accept'>";

    str += "<div class='challenge-front'>" +
        + "<div>"
        + `<img src='data:image/*;base64,${challange.coureImage}' alt='Avatar image'/>`
        + "div"
        + "<div class='challenge-content'>"
        + "<div class='challenge__avatar'>"
        + `<img src='data:image/*;base64,${challange.avatarImage}' alt='Avatar image'/>`
        + "</div>"
        + "<h3 class='challenge__username'>John Johnson</h3>"
        + "<p class='challenge__info'>Propose a challenge in Python language</p>"
        + "</div>"
        + " </div>";

    if (isCreate) str += "<div class='challenge-back challenge-back_accept'>";
    else str += "<div class='challenge-back challenge-back_accept'>";

    str += "<h3>Accept the competition</h3>"
        + "</div>"
        + "</div>"
        + "</a>"
        + "</div>";

    return str;
}

//<div class="swiper-slide">
//    <a href='#'>
//        <div class='challenge challenge_create'>
//            <div class='challenge-front'>
//               <div>
//                  <img class="challenge__image" src="~/images/cppCourse.png" alt="Avatar image"/>
//               </div>

//                <div class='challenge-content'>
//                    <div class='challenge__avatar'>
//                        <img src='images/avatarImg.png' alt='Avatar image' />
//                    </div>

//                    <h3 class='challenge__username'>John Johnson</h3>
//                    <p class='challenge__info'>Propose a challenge in Python language</p>
//                </div>
//            </div>

//            <div class='challenge-back challenge-back_create'>
//                <h3>Create the competition</h3>
//            </div>
//        </div>
//    </a>
//</div>

//<div class="swiper-slide">
//    <a href="#">
//        <div class="challenge challenge_accept">
//            <div class="challenge-front">
//                <div class="challenge__image" style="background-image: url('images/CSharp.jpg');">

//                </div>
//                <div class="challenge-content">
//                    <div class="challenge__avatar">
//                        <img src="images/avatarImg.png" alt="Avatar image" />
//                    </div>

//                    <h3 class="challenge__username">Jack Jackson</h3>
//                    <p class="challenge__info">Challenged you on the C# language</p>
//                </div>
//            </div>

//            <div class='challenge-back challenge-back_accept'>
//                <h3>Accept the competition</h3>
//            </div>
//        </div>
//    </a>
//</div>