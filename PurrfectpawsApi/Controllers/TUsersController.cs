using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurrfectpawsApi.Models;
using Microsoft.AspNetCore.Authorization;
using PurrfectpawsApi.DatabaseDbContext;

namespace PurrfectpawsApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TUsersController : ControllerBase
    {
        private readonly PurrfectpawsContext _context;

        public TUsersController(PurrfectpawsContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: api/TUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TUser>>> GetTUsers()
        {
            if (_context.TUsers == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // Ignore circular references
            };

            var usersWithAddresses = await _context.TUsers
                .Include(u => u.Role)
                .Include(u => u.ShippingAddresses)
                .Include(u => u.BillingAddresses)
                .ToListAsync();

            if (usersWithAddresses == null || usersWithAddresses.Count == 0)
            {
                return NotFound();
            }

            var result = usersWithAddresses.Select(user => new TUserGetsDTO
            {
                UserId = user.UserId,
                Role = user.Role?.RoleName ?? "",
                Name = user.Name,
                Email = user.Email,
                ShippingAddress = (List<TShippingAddress>)user.ShippingAddresses.Select(sa => new TShippingAddress
                {
                    ShippingAddressId = sa.ShippingAddressId,
                    UserId = sa.UserId,
                    Street1 = sa.Street1,
                    Street2 = sa.Street2,
                    City = sa.City,
                    State = sa.State,
                    Postcode = sa.Postcode
                }).ToList(),
                BillingAddresses = (List<TBillingAddress>)user.BillingAddresses.Select(ba => new TBillingAddress
                {
                    BillingAddressId = ba.BillingAddressId,
                    UserId = ba.UserId,
                    Street1 = ba.Street1,
                    Street2 = ba.Street2,
                    City = ba.City,
                    State = ba.State,
                    Postcode = ba.Postcode
                }).ToList()
            });

            // Serialize the result using the specified options
            var jsonString = JsonSerializer.Serialize(result, options);

            return Content(jsonString, "application/json");
        }

        [Authorize]
        // GET: api/TUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TUser>> GetTUser(int id)
        {
          if (_context.TUsers == null)
          {
              return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // Ignore circular references
            };

            var user = await _context.TUsers
                .Include(u => u.Role)
                .Include(u => u.ShippingAddresses)
                .Include(u => u.BillingAddresses)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            var result = new TUserGetsDTO
            {
                UserId = user.UserId,
                Role = user.Role?.RoleName ?? "",
                Name = user.Name,
                Email = user.Email,
                ShippingAddress = user.ShippingAddresses.Select(sa => new TShippingAddress
                {
                    ShippingAddressId = sa.ShippingAddressId,
                    UserId = sa.UserId,
                    Street1 = sa.Street1,
                    Street2 = sa.Street2,
                    City = sa.City,
                    State = sa.State,
                    Postcode = sa.Postcode
                }).ToList(),
                BillingAddresses = user.BillingAddresses.Select(ba => new TBillingAddress
                {
                    BillingAddressId = ba.BillingAddressId,
                    UserId = ba.UserId,
                    Street1 = ba.Street1,
                    Street2 = ba.Street2,
                    City = ba.City,
                    State = ba.State,
                    Postcode = ba.Postcode
                }).ToList()
            };

            // Serialize the result using the specified options
            var jsonString = JsonSerializer.Serialize(result, options);

            return Content(jsonString, "application/json");
        }

        [Authorize]
        // PUT: api/TUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTUser(int id, TUserPutDto tUserPutDto )
        {
            try
            {
                var user = await _context.TUsers.FirstOrDefaultAsync(u => u.UserId == id);
                var shippingAddress = await _context.TShippingAddresses.FirstOrDefaultAsync(s => s.UserId == id);
                var billingAddress = await _context.TBillingAddresses.FirstOrDefaultAsync(b => b.UserId == id);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                //_context.Entry(tUser).State = EntityState.Modified;
                user.Name = tUserPutDto.name;
                user.Email = tUserPutDto.email;
                if (shippingAddress != null)
                {
                    shippingAddress.Street1 = tUserPutDto.street_1;
                    shippingAddress.Street2 = tUserPutDto?.street_2;
                    shippingAddress.City = tUserPutDto.city;
                    shippingAddress.State = tUserPutDto.state;
                    shippingAddress.Postcode = tUserPutDto.postcode;
                }

                if (billingAddress != null)
                {
                    if (tUserPutDto.isBillingAddressSame == true)
                    {
                        billingAddress.Street1 = tUserPutDto.street_1;
                        billingAddress.Street2 = tUserPutDto?.street_2;
                        billingAddress.City = tUserPutDto.city;
                        billingAddress.State = tUserPutDto.state;
                        billingAddress.Postcode = tUserPutDto.postcode;
                    }
                    else
                    {
                        billingAddress.Street1 = tUserPutDto.billingStreet_1;
                        billingAddress.Street2 = tUserPutDto?.billingStreet_2;
                        billingAddress.City = tUserPutDto.billingCity;
                        billingAddress.State = tUserPutDto.billingState;
                        billingAddress.Postcode = tUserPutDto.billingPostcode;
                    }
                }


                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Register")]
        public async Task<ActionResult<TUser>> PostTUser([FromBody] TUserDTO tUserDTO)
        {
          if (_context.TUsers == null)
          {
              return Problem("Entity set 'PurrfectpawsContext.TUsers'  is null.");
          }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // Ignore circular references
            };

            var roleId = _context.MRoles.FirstOrDefault(r => r.RoleName == tUserDTO.role);
            if (roleId == null)
            {
                return NotFound("Role not found!");
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(tUserDTO.password);
            var tUser = new TUser
            {
                RoleId = roleId.RoleId,
                Name = tUserDTO.name,
                Email = tUserDTO.email,
                Password = hashedPassword
            };
            _context.TUsers.Add(tUser);
            await _context.SaveChangesAsync();
            var shippingAddress = new TShippingAddress
            {
                UserId = tUser.UserId,
                Street1 = tUserDTO.street_1,
                Street2 = tUserDTO.street_2,
                City = tUserDTO.city,
                State = tUserDTO.state,
                Postcode = tUserDTO.postcode
            };
            _context.TShippingAddresses.Add(shippingAddress);
            if (tUserDTO.isBillingAddressSame == true)
            {
                var billingAddress = new TBillingAddress
                {
                    UserId = tUser.UserId,
                    Street1 = tUserDTO.street_1,
                    Street2 = tUserDTO.street_2,
                    City = tUserDTO.city,
                    State = tUserDTO.state,
                    Postcode = tUserDTO.postcode
                };
                _context.TBillingAddresses.Add(billingAddress);
            }
            else
            {
                var billingAddress = new TBillingAddress
                {
                    UserId = tUser.UserId,
                    Street1 = tUserDTO.billingStreet_1,
                    Street2 = tUserDTO.billingStreet_2,
                    City = tUserDTO.billingCity,
                    State = tUserDTO.billingState,
                    Postcode = tUserDTO.billingPostcode
                };
                _context.TBillingAddresses.Add(billingAddress);
            }

            await _context.SaveChangesAsync();

            //var result = CreatedAtAction("GetTUser", new { id = tUser.UserId }, tUser);
            //// Serialize the result using the specified options
            //var jsonString = JsonSerializer.Serialize(result, options);

            return Ok("success");
            //return CreatedAtAction("GetTUser", new { id = tUser.UserId }, tUser);
        }

        [Authorize]
        // DELETE: api/TUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTUser(int id)
        {
            if (_context.TUsers == null)
            {
                return NotFound();
            }
            var tUser = await _context.TUsers.FindAsync(id);
            if (tUser == null)
            {
                return NotFound();
            }
            var shippingAddressToRemove = _context.TShippingAddresses.Where(s => s.UserId == tUser.UserId);
            var billingAddressToRemove = _context.TBillingAddresses.Where(s => s.UserId == tUser.UserId);
            _context.TUsers.Remove(tUser);
            _context.TShippingAddresses.RemoveRange(shippingAddressToRemove);
            _context.TBillingAddresses.RemoveRange(billingAddressToRemove);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TUserExists(int id)
        {
            return (_context.TUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
