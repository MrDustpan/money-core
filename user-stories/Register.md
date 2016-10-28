## User Registration

#### User Story
As a potential user of the site
I want the ability to self-register
So I can start using the features of the site

#### Acceptance Criteria
GIVEN I am on the registration page
AND I have entered my email address, password and confirm password
WHEN I click Register
THEN a confirmation email is sent to my email address
AND I am sent to a screen with instructions to check my email

GIVEN I am on the registration page
AND I have not entered my email address
WHEN I click Register
THEN an error message is displayed informing me that email address is required

GIVEN I am on the registration page
AND I have entered my email address but no password
WHEN I click Register
THEN an error message is displayed informing me that email address is required