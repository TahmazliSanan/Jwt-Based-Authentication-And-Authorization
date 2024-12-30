<div>
  <h1>
    Authentication and Authorization using JWT in .NET
  </h1>
</div>

<div>
  <p>
    <ul>
      <li>
        <b>Authentication</b> - the process of verifying the identity of a user or system.
      </li>
      <li>
        <b>Authorization</b> - the process of determining whether a user or system has permission to access a specific resource or perform a specific action.
      </li>
    </ul>
  </p>
  
  <p>
    I created JWT based authentication and authorization system in ASP.NET Core. JWT stands for <b>J</b>SON <b>W</b>eb <b>T</b>oken. <br>
    It keeps 3 main parts in itself:
    <ol>
      <li>
        <b>Header</b> - consists information about algorithm and token type.
      </li>
      <li>
        <b>Payload</b> - consists information about user and token's data.
      </li>
      <li>
        <b>Signature</b> - consists information about encoded signing key.
      </li>
    </ol>
  </p>

  <p>
    Firstly, we must create method which generate JWT and return it. When user login to system and if there is not any exception, JWT is generated and it is returned to us.
    <b>Authentication</b> stage verifies and validates this token based on its payload. <br>
    After this process <b>Authorization</b> stage is being and it determines whether the logged in user has access to the endpoint from which the request was sent. <br>
    If we want to implement JWT based authentication and authorization, we must use <code><b>AddAuthentication()</b></code> and <code><b>AddAuthorization()</b></code> methods.
    According to these we must use <code><b>UseAuthentication()</b></code> and <code><b>UseAuthorization()</b></code> methods for correct working of methods.
  </p>

  <h3>
    Installed NuGet Packages
  </h3>
  <hr>
  <p>
    <ul>
      <li>
        FluentValidation.AspNetCore (v-11.3.0)
      </li>
      <li>
        Microsoft.AspNetCore.Authentication.JwtBearer (v-6.0.36)
      </li>
      <li>
        Microsoft.EntityFrameworkCore (v-6.0.36)
      </li>
      <li>
        Microsoft.EntityFrameworkCore.Tools (v-6.0.36)
      </li>
      <li>
        Newtonsoft.Json (v-13.0.3)
      </li>
      <li>
        Npgsql.EntityFrameworkCore.PostgreSQL (v-6.0.29)
      </li>
      <li>
        Swashbuckle.AspNetCore (v-6.5.0)
      </li>
    </ul>
  </p>
</div>
