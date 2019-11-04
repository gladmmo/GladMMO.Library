﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum BaseObjectField
	{
		UNIT_FIELD_DISPLAYID = 0, // Size: 1, Type: INT, Flags: PUBLIC
		OBJECT_FIELD_SCALE_X = 1, // Size: 1, Type: FLOAT, Flags: PUBLIC
		UNIT_FIELD_LEVEL = 2, // Size: 1, Type: INT, Flags: PUBLIC
		RESERVED_1 = 3,
		RESERVED_2 = 4,
		RESERVED_3 = 5,
		RESERVED_4 = 6,
		RESERVED_5 = 7,

		//The end of the base fields.
		OBJECT_END = RESERVED_5,
	}

	public enum GameObjectField
	{
		/// <summary>
		/// The <see cref="GameObjectType"/>.
		/// </summary>
		GAMEOBJECT_TYPE_ID = BaseObjectField.OBJECT_END + 1, // Size: 1, Type: INT, Flags: PUBLIC
		RESERVED_DATA_1 = BaseObjectField.OBJECT_END + 2
	}

	public enum EntityObjectField
	{
		UNIT_FIELD_HEALTH = BaseObjectField.OBJECT_END + 1, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MAXHEALTH = BaseObjectField.OBJECT_END + 2, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_TARGET = BaseObjectField.OBJECT_END + 3, // Size: 2, Type: LONG, Flags: PUBLIC
		UNIT_FIELD_CASTING_SPELLID = BaseObjectField.OBJECT_END + 5,
		UNIT_FIELD_CASTING_STARTTIME = BaseObjectField.OBJECT_END + 6, //8 byte long UTC time.

		UNIT_END = UNIT_FIELD_CASTING_STARTTIME + 1, //UNIT_FIELD_CASTING_STARTTIME is 8 bytes
	}

	public enum PlayerObjectField
	{
		PLAYER_TOTAL_EXPERIENCE = EntityObjectField.UNIT_END + 1,
	}

	//TODO: We should move away from this and implement our own, these are just place holders from World of Warcraft/TrinityCore.
	//These public enums are the update field public enums taken from Trinitycore 3.3.5
	/*public enum EObjectFields
	{
		OBJECT_FIELD_GUID = 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
		OBJECT_FIELD_TYPE = 0x0002, // Size: 1, Type: INT, Flags: PUBLIC
		OBJECT_FIELD_ENTRY = 0x0003, // Size: 1, Type: INT, Flags: PUBLIC
		OBJECT_FIELD_SCALE_X = 0x0004, // Size: 1, Type: FLOAT, Flags: PUBLIC
		OBJECT_FIELD_PADDING = 0x0005, // Size: 1, Type: INT, Flags: NONE
		OBJECT_END = 0x0006
	};

	public enum EUnitFields
	{
		UNIT_FIELD_CHARM = EObjectFields.OBJECT_END + 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
		UNIT_FIELD_SUMMON = EObjectFields.OBJECT_END + 0x0002, // Size: 2, Type: LONG, Flags: PUBLIC
		UNIT_FIELD_CRITTER = EObjectFields.OBJECT_END + 0x0004, // Size: 2, Type: LONG, Flags: PRIVATE
		UNIT_FIELD_CHARMEDBY = EObjectFields.OBJECT_END + 0x0006, // Size: 2, Type: LONG, Flags: PUBLIC
		UNIT_FIELD_SUMMONEDBY = EObjectFields.OBJECT_END + 0x0008, // Size: 2, Type: LONG, Flags: PUBLIC
		UNIT_FIELD_CREATEDBY = EObjectFields.OBJECT_END + 0x000A, // Size: 2, Type: LONG, Flags: PUBLIC
		UNIT_FIELD_TARGET = EObjectFields.OBJECT_END + 0x000C, // Size: 2, Type: LONG, Flags: PUBLIC
		UNIT_FIELD_CHANNEL_OBJECT = EObjectFields.OBJECT_END + 0x000E, // Size: 2, Type: LONG, Flags: PUBLIC
		UNIT_CHANNEL_SPELL = EObjectFields.OBJECT_END + 0x0010, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_BYTES_0 = EObjectFields.OBJECT_END + 0x0011, // Size: 1, Type: BYTES, Flags: PUBLIC
		UNIT_FIELD_HEALTH = EObjectFields.OBJECT_END + 0x0012, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_POWER1 = EObjectFields.OBJECT_END + 0x0013, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_POWER2 = EObjectFields.OBJECT_END + 0x0014, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_POWER3 = EObjectFields.OBJECT_END + 0x0015, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_POWER4 = EObjectFields.OBJECT_END + 0x0016, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_POWER5 = EObjectFields.OBJECT_END + 0x0017, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_POWER6 = EObjectFields.OBJECT_END + 0x0018, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_POWER7 = EObjectFields.OBJECT_END + 0x0019, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MAXHEALTH = EObjectFields.OBJECT_END + 0x001A, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MAXPOWER1 = EObjectFields.OBJECT_END + 0x001B, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MAXPOWER2 = EObjectFields.OBJECT_END + 0x001C, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MAXPOWER3 = EObjectFields.OBJECT_END + 0x001D, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MAXPOWER4 = EObjectFields.OBJECT_END + 0x001E, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MAXPOWER5 = EObjectFields.OBJECT_END + 0x001F, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MAXPOWER6 = EObjectFields.OBJECT_END + 0x0020, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MAXPOWER7 = EObjectFields.OBJECT_END + 0x0021, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER = EObjectFields.OBJECT_END + 0x0022, // Size: 7, Type: FLOAT, Flags: PRIVATE, OWNER
		UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER = EObjectFields.OBJECT_END + 0x0029, // Size: 7, Type: FLOAT, Flags: PRIVATE, OWNER
		UNIT_FIELD_LEVEL = EObjectFields.OBJECT_END + 0x0030, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_FACTIONTEMPLATE = EObjectFields.OBJECT_END + 0x0031, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_VIRTUAL_ITEM_SLOT_ID = EObjectFields.OBJECT_END + 0x0032, // Size: 3, Type: INT, Flags: PUBLIC
		UNIT_FIELD_FLAGS = EObjectFields.OBJECT_END + 0x0035, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_FLAGS_2 = EObjectFields.OBJECT_END + 0x0036, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_AURASTATE = EObjectFields.OBJECT_END + 0x0037, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_BASEATTACKTIME = EObjectFields.OBJECT_END + 0x0038, // Size: 2, Type: INT, Flags: PUBLIC
		UNIT_FIELD_RANGEDATTACKTIME = EObjectFields.OBJECT_END + 0x003A, // Size: 1, Type: INT, Flags: PRIVATE
		UNIT_FIELD_BOUNDINGRADIUS = EObjectFields.OBJECT_END + 0x003B, // Size: 1, Type: FLOAT, Flags: PUBLIC
		UNIT_FIELD_COMBATREACH = EObjectFields.OBJECT_END + 0x003C, // Size: 1, Type: FLOAT, Flags: PUBLIC
		UNIT_FIELD_DISPLAYID = EObjectFields.OBJECT_END + 0x003D, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_NATIVEDISPLAYID = EObjectFields.OBJECT_END + 0x003E, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MOUNTDISPLAYID = EObjectFields.OBJECT_END + 0x003F, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_MINDAMAGE = EObjectFields.OBJECT_END + 0x0040, // Size: 1, Type: FLOAT, Flags: PRIVATE, OWNER, PARTY_LEADER
		UNIT_FIELD_MAXDAMAGE = EObjectFields.OBJECT_END + 0x0041, // Size: 1, Type: FLOAT, Flags: PRIVATE, OWNER, PARTY_LEADER
		UNIT_FIELD_MINOFFHANDDAMAGE = EObjectFields.OBJECT_END + 0x0042, // Size: 1, Type: FLOAT, Flags: PRIVATE, OWNER, PARTY_LEADER
		UNIT_FIELD_MAXOFFHANDDAMAGE = EObjectFields.OBJECT_END + 0x0043, // Size: 1, Type: FLOAT, Flags: PRIVATE, OWNER, PARTY_LEADER
		UNIT_FIELD_BYTES_1 = EObjectFields.OBJECT_END + 0x0044, // Size: 1, Type: BYTES, Flags: PUBLIC
		UNIT_FIELD_PETNUMBER = EObjectFields.OBJECT_END + 0x0045, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_PET_NAME_TIMESTAMP = EObjectFields.OBJECT_END + 0x0046, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_PETEXPERIENCE = EObjectFields.OBJECT_END + 0x0047, // Size: 1, Type: INT, Flags: OWNER
		UNIT_FIELD_PETNEXTLEVELEXP = EObjectFields.OBJECT_END + 0x0048, // Size: 1, Type: INT, Flags: OWNER
		UNIT_DYNAMIC_FLAGS = EObjectFields.OBJECT_END + 0x0049, // Size: 1, Type: INT, Flags: DYNAMIC
		UNIT_MOD_CAST_SPEED = EObjectFields.OBJECT_END + 0x004A, // Size: 1, Type: FLOAT, Flags: PUBLIC
		UNIT_CREATED_BY_SPELL = EObjectFields.OBJECT_END + 0x004B, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_NPC_FLAGS = EObjectFields.OBJECT_END + 0x004C, // Size: 1, Type: INT, Flags: DYNAMIC
		UNIT_NPC_EMOTESTATE = EObjectFields.OBJECT_END + 0x004D, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_STAT0 = EObjectFields.OBJECT_END + 0x004E, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_STAT1 = EObjectFields.OBJECT_END + 0x004F, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_STAT2 = EObjectFields.OBJECT_END + 0x0050, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_STAT3 = EObjectFields.OBJECT_END + 0x0051, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_STAT4 = EObjectFields.OBJECT_END + 0x0052, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_POSSTAT0 = EObjectFields.OBJECT_END + 0x0053, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_POSSTAT1 = EObjectFields.OBJECT_END + 0x0054, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_POSSTAT2 = EObjectFields.OBJECT_END + 0x0055, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_POSSTAT3 = EObjectFields.OBJECT_END + 0x0056, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_POSSTAT4 = EObjectFields.OBJECT_END + 0x0057, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_NEGSTAT0 = EObjectFields.OBJECT_END + 0x0058, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_NEGSTAT1 = EObjectFields.OBJECT_END + 0x0059, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_NEGSTAT2 = EObjectFields.OBJECT_END + 0x005A, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_NEGSTAT3 = EObjectFields.OBJECT_END + 0x005B, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_NEGSTAT4 = EObjectFields.OBJECT_END + 0x005C, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_RESISTANCES = EObjectFields.OBJECT_END + 0x005D, // Size: 7, Type: INT, Flags: PRIVATE, OWNER, PARTY_LEADER
		UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE = EObjectFields.OBJECT_END + 0x0064, // Size: 7, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE = EObjectFields.OBJECT_END + 0x006B, // Size: 7, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_BASE_MANA = EObjectFields.OBJECT_END + 0x0072, // Size: 1, Type: INT, Flags: PUBLIC
		UNIT_FIELD_BASE_HEALTH = EObjectFields.OBJECT_END + 0x0073, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_BYTES_2 = EObjectFields.OBJECT_END + 0x0074, // Size: 1, Type: BYTES, Flags: PUBLIC
		UNIT_FIELD_ATTACK_POWER = EObjectFields.OBJECT_END + 0x0075, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_ATTACK_POWER_MODS = EObjectFields.OBJECT_END + 0x0076, // Size: 1, Type: TWO_SHORT, Flags: PRIVATE, OWNER
		UNIT_FIELD_ATTACK_POWER_MULTIPLIER = EObjectFields.OBJECT_END + 0x0077, // Size: 1, Type: FLOAT, Flags: PRIVATE, OWNER
		UNIT_FIELD_RANGED_ATTACK_POWER = EObjectFields.OBJECT_END + 0x0078, // Size: 1, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_RANGED_ATTACK_POWER_MODS = EObjectFields.OBJECT_END + 0x0079, // Size: 1, Type: TWO_SHORT, Flags: PRIVATE, OWNER
		UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = EObjectFields.OBJECT_END + 0x007A, // Size: 1, Type: FLOAT, Flags: PRIVATE, OWNER
		UNIT_FIELD_MINRANGEDDAMAGE = EObjectFields.OBJECT_END + 0x007B, // Size: 1, Type: FLOAT, Flags: PRIVATE, OWNER
		UNIT_FIELD_MAXRANGEDDAMAGE = EObjectFields.OBJECT_END + 0x007C, // Size: 1, Type: FLOAT, Flags: PRIVATE, OWNER
		UNIT_FIELD_POWER_COST_MODIFIER = EObjectFields.OBJECT_END + 0x007D, // Size: 7, Type: INT, Flags: PRIVATE, OWNER
		UNIT_FIELD_POWER_COST_MULTIPLIER = EObjectFields.OBJECT_END + 0x0084, // Size: 7, Type: FLOAT, Flags: PRIVATE, OWNER
		UNIT_FIELD_MAXHEALTHMODIFIER = EObjectFields.OBJECT_END + 0x008B, // Size: 1, Type: FLOAT, Flags: PRIVATE, OWNER
		UNIT_FIELD_HOVERHEIGHT = EObjectFields.OBJECT_END + 0x008C, // Size: 1, Type: FLOAT, Flags: PUBLIC
		UNIT_FIELD_PADDING = EObjectFields.OBJECT_END + 0x008D, // Size: 1, Type: INT, Flags: NONE
		UNIT_END = EObjectFields.OBJECT_END + 0x008E,

		PLAYER_DUEL_ARBITER = UNIT_END + 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
		PLAYER_FLAGS = UNIT_END + 0x0002, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_GUILDID = UNIT_END + 0x0003, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_GUILDRANK = UNIT_END + 0x0004, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_BYTES = UNIT_END + 0x0005, // Size: 1, Type: BYTES, Flags: PUBLIC
		PLAYER_BYTES_2 = UNIT_END + 0x0006, // Size: 1, Type: BYTES, Flags: PUBLIC
		PLAYER_BYTES_3 = UNIT_END + 0x0007, // Size: 1, Type: BYTES, Flags: PUBLIC
		PLAYER_DUEL_TEAM = UNIT_END + 0x0008, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_GUILD_TIMESTAMP = UNIT_END + 0x0009, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_QUEST_LOG_1_1 = UNIT_END + 0x000A, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_1_2 = UNIT_END + 0x000B, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_1_3 = UNIT_END + 0x000C, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_1_4 = UNIT_END + 0x000E, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_2_1 = UNIT_END + 0x000F, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_2_2 = UNIT_END + 0x0010, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_2_3 = UNIT_END + 0x0011, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_2_5 = UNIT_END + 0x0013, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_3_1 = UNIT_END + 0x0014, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_3_2 = UNIT_END + 0x0015, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_3_3 = UNIT_END + 0x0016, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_3_5 = UNIT_END + 0x0018, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_4_1 = UNIT_END + 0x0019, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_4_2 = UNIT_END + 0x001A, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_4_3 = UNIT_END + 0x001B, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_4_5 = UNIT_END + 0x001D, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_5_1 = UNIT_END + 0x001E, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_5_2 = UNIT_END + 0x001F, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_5_3 = UNIT_END + 0x0020, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_5_5 = UNIT_END + 0x0022, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_6_1 = UNIT_END + 0x0023, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_6_2 = UNIT_END + 0x0024, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_6_3 = UNIT_END + 0x0025, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_6_5 = UNIT_END + 0x0027, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_7_1 = UNIT_END + 0x0028, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_7_2 = UNIT_END + 0x0029, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_7_3 = UNIT_END + 0x002A, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_7_5 = UNIT_END + 0x002C, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_8_1 = UNIT_END + 0x002D, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_8_2 = UNIT_END + 0x002E, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_8_3 = UNIT_END + 0x002F, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_8_5 = UNIT_END + 0x0031, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_9_1 = UNIT_END + 0x0032, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_9_2 = UNIT_END + 0x0033, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_9_3 = UNIT_END + 0x0034, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_9_5 = UNIT_END + 0x0036, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_10_1 = UNIT_END + 0x0037, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_10_2 = UNIT_END + 0x0038, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_10_3 = UNIT_END + 0x0039, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_10_5 = UNIT_END + 0x003B, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_11_1 = UNIT_END + 0x003C, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_11_2 = UNIT_END + 0x003D, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_11_3 = UNIT_END + 0x003E, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_11_5 = UNIT_END + 0x0040, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_12_1 = UNIT_END + 0x0041, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_12_2 = UNIT_END + 0x0042, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_12_3 = UNIT_END + 0x0043, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_12_5 = UNIT_END + 0x0045, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_13_1 = UNIT_END + 0x0046, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_13_2 = UNIT_END + 0x0047, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_13_3 = UNIT_END + 0x0048, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_13_5 = UNIT_END + 0x004A, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_14_1 = UNIT_END + 0x004B, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_14_2 = UNIT_END + 0x004C, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_14_3 = UNIT_END + 0x004D, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_14_5 = UNIT_END + 0x004F, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_15_1 = UNIT_END + 0x0050, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_15_2 = UNIT_END + 0x0051, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_15_3 = UNIT_END + 0x0052, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_15_5 = UNIT_END + 0x0054, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_16_1 = UNIT_END + 0x0055, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_16_2 = UNIT_END + 0x0056, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_16_3 = UNIT_END + 0x0057, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_16_5 = UNIT_END + 0x0059, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_17_1 = UNIT_END + 0x005A, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_17_2 = UNIT_END + 0x005B, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_17_3 = UNIT_END + 0x005C, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_17_5 = UNIT_END + 0x005E, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_18_1 = UNIT_END + 0x005F, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_18_2 = UNIT_END + 0x0060, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_18_3 = UNIT_END + 0x0061, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_18_5 = UNIT_END + 0x0063, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_19_1 = UNIT_END + 0x0064, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_19_2 = UNIT_END + 0x0065, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_19_3 = UNIT_END + 0x0066, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_19_5 = UNIT_END + 0x0068, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_20_1 = UNIT_END + 0x0069, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_20_2 = UNIT_END + 0x006A, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_20_3 = UNIT_END + 0x006B, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_20_5 = UNIT_END + 0x006D, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_21_1 = UNIT_END + 0x006E, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_21_2 = UNIT_END + 0x006F, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_21_3 = UNIT_END + 0x0070, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_21_5 = UNIT_END + 0x0072, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_22_1 = UNIT_END + 0x0073, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_22_2 = UNIT_END + 0x0074, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_22_3 = UNIT_END + 0x0075, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_22_5 = UNIT_END + 0x0077, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_23_1 = UNIT_END + 0x0078, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_23_2 = UNIT_END + 0x0079, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_23_3 = UNIT_END + 0x007A, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_23_5 = UNIT_END + 0x007C, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_24_1 = UNIT_END + 0x007D, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_24_2 = UNIT_END + 0x007E, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_24_3 = UNIT_END + 0x007F, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_24_5 = UNIT_END + 0x0081, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_25_1 = UNIT_END + 0x0082, // Size: 1, Type: INT, Flags: PARTY_MEMBER
		PLAYER_QUEST_LOG_25_2 = UNIT_END + 0x0083, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_QUEST_LOG_25_3 = UNIT_END + 0x0084, // Size: 2, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_QUEST_LOG_25_5 = UNIT_END + 0x0086, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_VISIBLE_ITEM_1_ENTRYID = UNIT_END + 0x0087, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_1_ENCHANTMENT = UNIT_END + 0x0088, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_2_ENTRYID = UNIT_END + 0x0089, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_2_ENCHANTMENT = UNIT_END + 0x008A, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_3_ENTRYID = UNIT_END + 0x008B, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_3_ENCHANTMENT = UNIT_END + 0x008C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_4_ENTRYID = UNIT_END + 0x008D, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_4_ENCHANTMENT = UNIT_END + 0x008E, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_5_ENTRYID = UNIT_END + 0x008F, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_5_ENCHANTMENT = UNIT_END + 0x0090, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_6_ENTRYID = UNIT_END + 0x0091, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_6_ENCHANTMENT = UNIT_END + 0x0092, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_7_ENTRYID = UNIT_END + 0x0093, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_7_ENCHANTMENT = UNIT_END + 0x0094, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_8_ENTRYID = UNIT_END + 0x0095, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_8_ENCHANTMENT = UNIT_END + 0x0096, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_9_ENTRYID = UNIT_END + 0x0097, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_9_ENCHANTMENT = UNIT_END + 0x0098, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_10_ENTRYID = UNIT_END + 0x0099, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_10_ENCHANTMENT = UNIT_END + 0x009A, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_11_ENTRYID = UNIT_END + 0x009B, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_11_ENCHANTMENT = UNIT_END + 0x009C, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_12_ENTRYID = UNIT_END + 0x009D, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_12_ENCHANTMENT = UNIT_END + 0x009E, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_13_ENTRYID = UNIT_END + 0x009F, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_13_ENCHANTMENT = UNIT_END + 0x00A0, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_14_ENTRYID = UNIT_END + 0x00A1, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_14_ENCHANTMENT = UNIT_END + 0x00A2, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_15_ENTRYID = UNIT_END + 0x00A3, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_15_ENCHANTMENT = UNIT_END + 0x00A4, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_16_ENTRYID = UNIT_END + 0x00A5, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_16_ENCHANTMENT = UNIT_END + 0x00A6, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_17_ENTRYID = UNIT_END + 0x00A7, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_17_ENCHANTMENT = UNIT_END + 0x00A8, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_18_ENTRYID = UNIT_END + 0x00A9, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_18_ENCHANTMENT = UNIT_END + 0x00AA, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_19_ENTRYID = UNIT_END + 0x00AB, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_VISIBLE_ITEM_19_ENCHANTMENT = UNIT_END + 0x00AC, // Size: 1, Type: TWO_SHORT, Flags: PUBLIC
		PLAYER_CHOSEN_TITLE = UNIT_END + 0x00AD, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_FAKE_INEBRIATION = UNIT_END + 0x00AE, // Size: 1, Type: INT, Flags: PUBLIC
		PLAYER_FIELD_PAD_0 = UNIT_END + 0x00AF, // Size: 1, Type: INT, Flags: NONE
		PLAYER_FIELD_INV_SLOT_HEAD = UNIT_END + 0x00B0, // Size: 46, Type: LONG, Flags: PRIVATE
		PLAYER_FIELD_PACK_SLOT_1 = UNIT_END + 0x00DE, // Size: 32, Type: LONG, Flags: PRIVATE
		PLAYER_FIELD_BANK_SLOT_1 = UNIT_END + 0x00FE, // Size: 56, Type: LONG, Flags: PRIVATE
		PLAYER_FIELD_BANKBAG_SLOT_1 = UNIT_END + 0x0136, // Size: 14, Type: LONG, Flags: PRIVATE
		PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = UNIT_END + 0x0144, // Size: 24, Type: LONG, Flags: PRIVATE
		PLAYER_FIELD_KEYRING_SLOT_1 = UNIT_END + 0x015C, // Size: 64, Type: LONG, Flags: PRIVATE
		PLAYER_FIELD_CURRENCYTOKEN_SLOT_1 = UNIT_END + 0x019C, // Size: 64, Type: LONG, Flags: PRIVATE
		PLAYER_FARSIGHT = UNIT_END + 0x01DC, // Size: 2, Type: LONG, Flags: PRIVATE
		PLAYER__FIELD_KNOWN_TITLES = UNIT_END + 0x01DE, // Size: 2, Type: LONG, Flags: PRIVATE
		PLAYER__FIELD_KNOWN_TITLES1 = UNIT_END + 0x01E0, // Size: 2, Type: LONG, Flags: PRIVATE
		PLAYER__FIELD_KNOWN_TITLES2 = UNIT_END + 0x01E2, // Size: 2, Type: LONG, Flags: PRIVATE
		PLAYER_FIELD_KNOWN_CURRENCIES = UNIT_END + 0x01E4, // Size: 2, Type: LONG, Flags: PRIVATE
		PLAYER_XP = UNIT_END + 0x01E6, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_NEXT_LEVEL_XP = UNIT_END + 0x01E7, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_SKILL_INFO_1_1 = UNIT_END + 0x01E8, // Size: 384, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_CHARACTER_POINTS1 = UNIT_END + 0x0368, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_CHARACTER_POINTS2 = UNIT_END + 0x0369, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_TRACK_CREATURES = UNIT_END + 0x036A, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_TRACK_RESOURCES = UNIT_END + 0x036B, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_BLOCK_PERCENTAGE = UNIT_END + 0x036C, // Size: 1, Type: FLOAT, Flags: PRIVATE
		PLAYER_DODGE_PERCENTAGE = UNIT_END + 0x036D, // Size: 1, Type: FLOAT, Flags: PRIVATE
		PLAYER_PARRY_PERCENTAGE = UNIT_END + 0x036E, // Size: 1, Type: FLOAT, Flags: PRIVATE
		PLAYER_EXPERTISE = UNIT_END + 0x036F, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_OFFHAND_EXPERTISE = UNIT_END + 0x0370, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_CRIT_PERCENTAGE = UNIT_END + 0x0371, // Size: 1, Type: FLOAT, Flags: PRIVATE
		PLAYER_RANGED_CRIT_PERCENTAGE = UNIT_END + 0x0372, // Size: 1, Type: FLOAT, Flags: PRIVATE
		PLAYER_OFFHAND_CRIT_PERCENTAGE = UNIT_END + 0x0373, // Size: 1, Type: FLOAT, Flags: PRIVATE
		PLAYER_SPELL_CRIT_PERCENTAGE1 = UNIT_END + 0x0374, // Size: 7, Type: FLOAT, Flags: PRIVATE
		PLAYER_SHIELD_BLOCK = UNIT_END + 0x037B, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE = UNIT_END + 0x037C, // Size: 1, Type: FLOAT, Flags: PRIVATE
		PLAYER_EXPLORED_ZONES_1 = UNIT_END + 0x037D, // Size: 128, Type: BYTES, Flags: PRIVATE
		PLAYER_REST_STATE_EXPERIENCE = UNIT_END + 0x03FD, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_COINAGE = UNIT_END + 0x03FE, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_MOD_DAMAGE_DONE_POS = UNIT_END + 0x03FF, // Size: 7, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = UNIT_END + 0x0406, // Size: 7, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = UNIT_END + 0x040D, // Size: 7, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_MOD_HEALING_DONE_POS = UNIT_END + 0x0414, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_MOD_HEALING_PCT = UNIT_END + 0x0415, // Size: 1, Type: FLOAT, Flags: PRIVATE
		PLAYER_FIELD_MOD_HEALING_DONE_PCT = UNIT_END + 0x0416, // Size: 1, Type: FLOAT, Flags: PRIVATE
		PLAYER_FIELD_MOD_TARGET_RESISTANCE = UNIT_END + 0x0417, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE = UNIT_END + 0x0418, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_BYTES = UNIT_END + 0x0419, // Size: 1, Type: BYTES, Flags: PRIVATE
		PLAYER_AMMO_ID = UNIT_END + 0x041A, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_SELF_RES_SPELL = UNIT_END + 0x041B, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_PVP_MEDALS = UNIT_END + 0x041C, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_BUYBACK_PRICE_1 = UNIT_END + 0x041D, // Size: 12, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = UNIT_END + 0x0429, // Size: 12, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_KILLS = UNIT_END + 0x0435, // Size: 1, Type: TWO_SHORT, Flags: PRIVATE
		PLAYER_FIELD_TODAY_CONTRIBUTION = UNIT_END + 0x0436, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_YESTERDAY_CONTRIBUTION = UNIT_END + 0x0437, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_LIFETIME_HONORABLE_KILLS = UNIT_END + 0x0438, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_BYTES2 = UNIT_END + 0x0439, // Size: 1, Type: 6, Flags: PRIVATE
		PLAYER_FIELD_WATCHED_FACTION_INDEX = UNIT_END + 0x043A, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_COMBAT_RATING_1 = UNIT_END + 0x043B, // Size: 25, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_ARENA_TEAM_INFO_1_1 = UNIT_END + 0x0454, // Size: 21, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_HONOR_CURRENCY = UNIT_END + 0x0469, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_ARENA_CURRENCY = UNIT_END + 0x046A, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_MAX_LEVEL = UNIT_END + 0x046B, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_DAILY_QUESTS_1 = UNIT_END + 0x046C, // Size: 25, Type: INT, Flags: PRIVATE
		PLAYER_RUNE_REGEN_1 = UNIT_END + 0x0485, // Size: 4, Type: FLOAT, Flags: PRIVATE
		PLAYER_NO_REAGENT_COST_1 = UNIT_END + 0x0489, // Size: 3, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_GLYPH_SLOTS_1 = UNIT_END + 0x048C, // Size: 6, Type: INT, Flags: PRIVATE
		PLAYER_FIELD_GLYPHS_1 = UNIT_END + 0x0492, // Size: 6, Type: INT, Flags: PRIVATE
		PLAYER_GLYPHS_ENABLED = UNIT_END + 0x0498, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_PET_SPELL_POWER = UNIT_END + 0x0499, // Size: 1, Type: INT, Flags: PRIVATE
		PLAYER_END = UNIT_END + 0x049A
	};

	public enum EGameObjectFields
	{
		OBJECT_FIELD_CREATED_BY = EObjectFields.OBJECT_END + 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
		GAMEOBJECT_DISPLAYID = EObjectFields.OBJECT_END + 0x0002, // Size: 1, Type: INT, Flags: PUBLIC
		GAMEOBJECT_FLAGS = EObjectFields.OBJECT_END + 0x0003, // Size: 1, Type: INT, Flags: PUBLIC
		GAMEOBJECT_PARENTROTATION = EObjectFields.OBJECT_END + 0x0004, // Size: 4, Type: FLOAT, Flags: PUBLIC
		GAMEOBJECT_DYNAMIC = EObjectFields.OBJECT_END + 0x0008, // Size: 1, Type: TWO_SHORT, Flags: DYNAMIC
		GAMEOBJECT_FACTION = EObjectFields.OBJECT_END + 0x0009, // Size: 1, Type: INT, Flags: PUBLIC
		GAMEOBJECT_LEVEL = EObjectFields.OBJECT_END + 0x000A, // Size: 1, Type: INT, Flags: PUBLIC
		GAMEOBJECT_BYTES_1 = EObjectFields.OBJECT_END + 0x000B, // Size: 1, Type: BYTES, Flags: PUBLIC
		OBJECT_END = EObjectFields.OBJECT_END + 0x000C
	};

	public enum EDynamicObjectFields
	{
		DYNAMICOBJECT_CASTER = EObjectFields.OBJECT_END + 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
		DYNAMICOBJECT_BYTES = EObjectFields.OBJECT_END + 0x0002, // Size: 1, Type: BYTES, Flags: PUBLIC
		DYNAMICOBJECT_SPELLID = EObjectFields.OBJECT_END + 0x0003, // Size: 1, Type: INT, Flags: PUBLIC
		DYNAMICOBJECT_RADIUS = EObjectFields.OBJECT_END + 0x0004, // Size: 1, Type: FLOAT, Flags: PUBLIC
		DYNAMICOBJECT_CASTTIME = EObjectFields.OBJECT_END + 0x0005, // Size: 1, Type: INT, Flags: PUBLIC
		OBJECT_END = EObjectFields.OBJECT_END + 0x0006
	};

	public enum ECorpseFields
	{
		CORPSE_FIELD_OWNER = EObjectFields.OBJECT_END + 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
		CORPSE_FIELD_PARTY = EObjectFields.OBJECT_END + 0x0002, // Size: 2, Type: LONG, Flags: PUBLIC
		CORPSE_FIELD_DISPLAY_ID = EObjectFields.OBJECT_END + 0x0004, // Size: 1, Type: INT, Flags: PUBLIC
		CORPSE_FIELD_ITEM = EObjectFields.OBJECT_END + 0x0005, // Size: 19, Type: INT, Flags: PUBLIC
		CORPSE_FIELD_BYTES_1 = EObjectFields.OBJECT_END + 0x0018, // Size: 1, Type: BYTES, Flags: PUBLIC
		CORPSE_FIELD_BYTES_2 = EObjectFields.OBJECT_END + 0x0019, // Size: 1, Type: BYTES, Flags: PUBLIC
		CORPSE_FIELD_GUILD = EObjectFields.OBJECT_END + 0x001A, // Size: 1, Type: INT, Flags: PUBLIC
		CORPSE_FIELD_FLAGS = EObjectFields.OBJECT_END + 0x001B, // Size: 1, Type: INT, Flags: PUBLIC
		CORPSE_FIELD_DYNAMIC_FLAGS = EObjectFields.OBJECT_END + 0x001C, // Size: 1, Type: INT, Flags: DYNAMIC
		CORPSE_FIELD_PAD = EObjectFields.OBJECT_END + 0x001D, // Size: 1, Type: INT, Flags: NONE
		CORPSE_END = EObjectFields.OBJECT_END + 0x001E
	};*/
}