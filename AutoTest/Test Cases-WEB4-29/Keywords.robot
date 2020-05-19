*** Keywords ***
Begin Web Test
    Open Browser                about:blank  ${BROWSER}
    Maximize Browser Window
    Set Selenium Speed          0.5

Go to Web Page
    Load page
    Verify startpage loaded

Load page
    Go To                       ${URL}

Verify start page loaded
    ${link_text} = 		        Get Text  xpath:/html/body/header/nav/div/a
    Should Be Equal		        ${link_text}  OMGZ SHOES

Log in with Valid E-mail
    Click Link                  xpath:/html/body/header/nav/div/div/ul/li[2]/a
    Input Text                  id:Email  alfred.josephalfred@gmail.com
    Input Text                  id:Password  Joe_777
    Click Button                xpath:/html/body/div/main/div[2]/div/form/div[3]/input

Verify Log in with valid E-mail
    ${link_text} =              Get Text  xpath:/html/body/header/nav/div/div/ul/li[2]/a
    Should Be Equal             ${link_text}  Logout

Switch to Shopping Cart
     Go to Web Page
     Click Link                 xpath://*[@id="AddToShoppingCart"]
     Click Link                 xpath:/html/body/header/nav/div/div/ul/li[1]/a
     Wait Until Page Contains   Total cost:

Verify Shopping Cart Loaded
    ${link_text} = 		         Get Text  xpath:/html/body/div/main/div/span/a
    Should Be Equal		         ${link_text}  Confirm Order

Verify Confirm Order
    Click Link                    Link:Confirm Order
    ${link_text} = 		          Get Text  xpath:/html/body/div/main/form/label[2]
    Should Be Equal		          ${link_text}  Delivery Time

Verify way of Payment and Delivery Option to Confirm order
    Click Element                 xpath://*[@id="paymentType"]/option[2]
    Click Element                 xpath://*[@id="deliveryTime"]/option[2]
    Click Element                 xpath://*[@id="paymentType"]/option[1]
    Click Element                 xpath://*[@id="deliveryTime"]/option[3]

Verify Swish Payment and Delivery Option to Confirm order then click on Shop More
    Click Element                 xpath://*[@id="paymentType"]/option[1]
    Click Element                 xpath://*[@id="deliveryTime"]/option[1]
    Input Text                    xpath:/html/body/div/main/form/input[1]  ${email_text}
    Click Button                  id:submit
    ${link_text} =                Get Text  xpath:/html/body/div/main/p[3]
    Should Be Equal               ${link_text}  This confirmation has also been sent to your email!
    Click Element                 xpath:/html/body/div/main/a[2]

Verify Invoice Payment and Delivery Option to Confirm order then click on Download as PDF
    Execute Javascript            window.scrollBy(0,400)
    Click Link                    xpath://*[@id="AddToShoppingCart"]
    Click Link                    xpath:/html/body/header/nav/div/div/ul/li[1]/a
    Wait Until Page Contains      Total cost:
    Click Link                    xpath:/html/body/div/main/div/span/a
    Click Element                 xpath://*[@id="paymentType"]/option[2]
    Click Element                 xpath://*[@id="deliveryTime"]/option[2]
    Input Text                    xpath:/html/body/div/main/form/input[1]  ${email_text}
    Click Button                  id:submit
    ${link_text} =                Get Text  xpath:/html/body/div/main/p[3]
    Should Be Equal               ${link_text}  This confirmation has also been sent to your email!
    Click Element                 xpath:/html/body/div/main/a[1]

End Web Test
    Close Browser