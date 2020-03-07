using Microsoft.EntityFrameworkCore.Migrations;
using System; using FreecraftCore;
using System.Collections.Generic;

namespace GladMMO.Database.GameServer.Migrations
{
	public partial class AddedStoredProcedures : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(
@"CREATE PROCEDURE `create_claimed_session`(IN accountId int, IN characterId int)
BEGIN
DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		ROLLBACK;  -- rollback any changes made in the transaction
		RESIGNAL;  -- raise again the sql exception to the caller
	END;

# Check if accountid and characterid are valid first. Second part is more expensive.
# This is more preformant because it avoids locks/transactions until we absolutely have to
IF (0 = (SELECT COUNT(*)
		FROM `guardians.gameserver`.characters ch
		WHERE ch.CharacterId = characterId AND ch.AccountId = accountId))
THEN
	SIGNAL SQLSTATE '45000'
	  SET MESSAGE_TEXT = 'Attempted session creation but AccountId and CharacterId do not align.';
ELSE
# If the accountId IS the accountId of the character then we need to start the transaction
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	START TRANSACTION;
		IF (0 = (SELECT COUNT(*)
			FROM `guardians.gameserver`.claimed_sessions CS
			INNER JOIN characters C ON C.CharacterId = CS.CharacterId
			WHERE C.AccountId = accountId))
		THEN
			INSERT INTO `guardians.gameserver`.claimed_sessions (CharacterId) VALUES(characterId);
		ELSE
			SIGNAL SQLSTATE '45000'
				SET MESSAGE_TEXT = 'Attempted session creation but an active session for this account already exists.';
		END IF;
		COMMIT;
END IF;
END");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{

		}
	}
}
