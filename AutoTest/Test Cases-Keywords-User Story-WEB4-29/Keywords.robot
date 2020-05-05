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

Verify Confirm Order
    Click Link                    Link:Confirm Order
    ${link_text} = 		          Get Text  xpath:/html/body/div/main/form/h2[1]
    Should Be Equal		          ${link_text}  Payment

Verify way of Payment and Delivery Option to Confirm order
    Select Checkbox               id:invoice
    Select Checkbox               id:fiveToTenDays
    Click Element                 xpath:/html/body/div/main/a
    Click Link                    Link:Confirm Order
    Select Checkbox               id:swish
    Select Checkbox               id:twoToFiveDay

Verify Swish Payment and Delivery Option to Confirm order then click on Shop More
    Select Checkbox               id:swish
    Select Checkbox               id:twoToFiveDay
    Input Text                    xpath:/html/body/div/main/form/input[6]  ${email_text}
    Click Button                  xpath://*[@id="submit"]
    ${link_text} =                Get Text  xpath:/html/body/div/main/p[3]
    Should Be Equal               ${link_text}  This confirmation has also been sent to your email!
    Click Element                 xpath:/html/body/div/main/a[1]

Verify Invoice Payment and Delivery Option to Confirm order then click on Download as PDF
    Click Element                 xpath:/html/body/div/main/table/tbody/tr[1]/td[6]/a[2]
    Click Link                    Link:Shopping Cart
    Click Link                    Link:Confirm Order
    Select Checkbox               id:invoice
    Select Checkbox               id:thirtyDays
    Input Text                    xpath:/html/body/div/main/form/input[6]  ${email_text}
    Click Button                  xpath://*[@id="submit"]
    ${link_text} =                Get Text  xpath:/html/body/div/main/p[3]
    Should Be Equal               ${link_text}  This confirmation has also been sent to your email!
    Click Element                 xpath:/html/body/div/main/a[2]

End Web Test
    Close Browser