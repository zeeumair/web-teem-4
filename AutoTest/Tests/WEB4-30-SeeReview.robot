*** Settings ***

Documentation                   This is some basic info about the whole OMGZ SHOES test suite
Resource                        ../Resources/Keywords.robot
Library                         SeleniumLibrary
Library                         DateTime

Suite Setup                     Begin Web Test
Suite Teardown                  End Web Test

*** Variables ***

${BROWSER} =                    chrome
${URL} =                        https://localhost:44304/

*** Test Cases ***

User can access Webshop site
    [Documentation]             Test: User should be able to access the Webshop main page
    [Tags]                      Access Webshop
        Go to Web Page

User can see Product Details without review and rating
    [Documentation]             Test: User should be informed if no reviews and rating exist on the product Details page
    [Tags]                      Product without review and rating

        Product without review and rating
        Varify product without review and rating

User can see Product Details with all reviews
    [Documentation]             Test: User should be able to see Product Details with all reviews
    [Tags]                      Product Details with all reviews
        Product Details with all reviews
        Varify Product Details with all reviews

User can see Product Details with average rating
    [Documentation]             Test: User should be able to see Product Details with average rating
    [Tags]                      Product Details with average rating
        Product Details with average rating
        Varify Product Details with average rating