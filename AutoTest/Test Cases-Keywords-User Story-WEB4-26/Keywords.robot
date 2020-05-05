*** Keywords ***
Begin Web Test
    Open Browser                about:blank  ${BROWSER}
    Maximize Browser Window
    Set Selenium Speed          0.9

Go to Web Page
    Load page
    Verify startpage loaded

Load page
    Go To                       ${URL}

Verify start page loaded
    ${link_text} = 		        Get Text  class:navbar-brand
    Should Be Equal		        ${link_text}  Webshop

Switch to Shopping Cart
     Go to Web Page
     Click Link                 Link:Shopping Cart
     Wait Until Page Contains   Total cost:

Verify Shopping Cart Loaded
    ${link_text} = 		         Get Text  class:btn btn btn-success
    Should Be Equal		         ${link_text}  Confirm Order

Confirm Order
    Click Button                xpath:/html/body/div/main/span/a
    Confirm

Verify adding products
    Click Button                xpath:/html/body/div/main/span/a
    ${expected1} =              Set Variable  Quantity number is increasing by pressing on "+ button"
    ${expected2} =              Set Variable  Price value of the chosen product is increasing by pressing on "+ button"
    ${actual} =                 Get Text  class:text-center
    Should Be Equal             ${expected1,2}  ${actual}

Remove products from Shopping Cart
    Click Button                xpath:/html/body/div/main/table/tbody/tr/td[2]/div/div[2]/form/input[3]
    Confirm removing

Verify removing products
    Click Button                xpath:/html/body/div/main/table/tbody/tr/td[2]/div/div[2]/form/input[3]
    ${expected1} =              Set Variable  Quantity number is decreasing by pressing on "- button"
    ${expected2} =              Set Variable  Price value of the chosen product is decreasing by pressing on "- button"
    ${actual} =                 Get Text  class:text-center
    Should Be Equal             ${expected1,2}  ${actual}

Empty Shopping Cart from any products
    [Documentation]
    wait until element is enabled   ${CLICK_btn btn-danger btn-sm py-0}
    click element               xpath:/html/body/div/main/table/tbody/tr/td[2]/div/div[2]/form/input[3]
    confirm action
    repeat keyword              5 times Go to Previous Page

Verify Empty shopping Cart
    Click Button                xpath:/html/body/div/main/table/tbody/tr/td[2]/div/div[2]/form/input[3]
    ${link_text} = 		        Get Text  class:float-right mr-2
    Should Be Equal		        ${link_text}  Confirm Order became "red" in color

Verify Back Tab Is Clickable
    Click Element               xpath:/html/body/div/main/a
    ${link_text} =              Get Text  <a href="/">Back</a>
    Should Be Equal             ${link_text}  Webshop "List of products"


End Web Test
    Close Browser


