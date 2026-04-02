<#import "template.ftl" as layout>

<#macro printFieldErrors field>
    <#if messagesPerField.existsError(field)>
        <span class="alert-error" style="display: block; margin-top: 4px; font-size: 12px;">
            ${messagesPerField.get(field)}
        </span>
    </#if>
</#macro>

<@layout.registrationLayout displayInfo=social.displayInfo displayMessage=false; section>
    <#if section = "header">
        <div class="login-header-wrapper">
            <a href="${properties.frontendUrl!"http://localhost:3000"}" class="back-link">ТРАМПЛИН</a>
        </div>
        <h1 class="title">Регистрация</h1>
    <#elseif section = "form">
        <#if message?has_content && !messagesPerField.existsError('firstName', 'lastName', 'email', 'password', 'password-confirm')>
            <div class="alert alert-${message.type}">
                <span class="message-text">${message.summary?no_esc}</span>
            </div>
        </#if>
        <form id="kc-register-form" action="${url.registrationAction}" method="post">
            <div class="form-group ${messagesPerField.existsError('email')?string('has-error', '')}">
                <label for="email" class="form-label">Электронная почта</label>
                <input type="email" id="email" name="email" class="form-input" value="${(register.formData.email!'')}" placeholder="e-mail@mail.ru">
                <@printFieldErrors "email"/>
            </div>

            <div class="form-group ${messagesPerField.existsError('password')?string('has-error', '')}">
                <label for="password" class="form-label">Пароль</label>
                <input type="password" id="password" name="password" class="form-input" placeholder="Пароль">
                <@printFieldErrors "password"/>
            </div>

            <div class="form-group ${messagesPerField.existsError('password-confirm')?string('has-error', '')}">
                <label for="password-confirm" class="form-label">Подтверждение пароля</label>
                <input type="password" id="password-confirm" name="password-confirm" class="form-input" placeholder="Повторите пароль">
                <@printFieldErrors "password-confirm"/>
            </div>

            <button class="btn-primary" type="submit">Зарегистрироваться</button>
        </form>
        <div class="login-footer">
            <p>Уже есть аккаунт? <a href="${url.loginUrl}">Войти</a></p>
        </div>
    </#if>
</@layout.registrationLayout>