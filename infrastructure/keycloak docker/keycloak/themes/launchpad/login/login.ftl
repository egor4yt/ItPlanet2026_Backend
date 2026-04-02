<#import "template.ftl" as layout>
<@layout.registrationLayout displayInfo=social.displayInfo displayMessage=false; section>
    <#if section = "header">
        <div class="login-header-wrapper">
            <a href="${properties.frontendUrl!"http://localhost:3000"}" class="back-link">ТРАМПЛИН</a>
        </div>
        <h1 class="title">Вход</h1>
    <#elseif section = "form">
    <#if message?has_content>
        <div class="alert alert-${message.type}">
            <span class="message-text">${message.summary?no_esc}</span>
        </div>
    </#if>
        <form id="kc-form-login" onsubmit="login.disabled = true; return true;" action="${url.loginAction}" method="post">
            <div class="form-group">
                <label for="username" class="form-label">Электронная почта</label>
                <input id="username" name="username" type="email" class="form-input" placeholder="e-mail@mail.ru" value="${(login.username!'')}">
            </div>

            <div class="form-group">
                <label for="password" class="form-label">Пароль</label>
                <input id="password" name="password" type="password" class="form-input" placeholder="Пароль">
            </div>

            <button class="btn-primary" name="login" id="kc-login" type="submit">Войти</button>
        </form>
        <div class="login-footer">
            <p>Нет аккаунта? <a href="${url.registrationUrl}">Зарегистрироваться</a></p>
        </div>
    </#if>
</@layout.registrationLayout>